using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Building : MonoBehaviour
{
    #region Fields
    public string identifier { get; protected set; }
    //Each Block also saves what Cell it is sitting on (which might be weird because the Cells remember what Block is sitting on them)
    public List<Block> blocks { get; protected set; }
    public ButtonData buttonData { get; protected set; }
    public bool placed { get; set; }
    protected Color color;
    private Transform modelTransform;
    public bool isSupport { get; protected set; }

    [Header("Button Data")]
    [SerializeField] private string buildingName;
    [SerializeField] private string description;
    [SerializeField] private Sprite image;
    [SerializeField] private Sprite shape;
    #endregion

    protected virtual void OnEnable()
    {
        buttonData = ScriptableObject.CreateInstance<ButtonData>();
        blocks = new();
        placed = false;
        modelTransform = transform.GetChild(0);

        buttonData.buildingName = buildingName;
        buttonData.buildingDescription = description;
        buttonData.buildingImage = image;
        buttonData.buildingShape = shape;
    }

    public void Rotate()
    {
        for (int i = 0; i < blocks.Count; i++)
        {
            Block b = blocks[i];
            b.pos = new Vector2Int(b.pos.y, -b.pos.x);
            blocks[i] = b;
        }

        modelTransform.Rotate(0, 90, 0);
    }
    protected void AddBlock(Block newBlock)
    {
        //If the given Block is already in the list, it won't be added again
        if (!blocks.Contains(newBlock))
        {
            blocks.Add(newBlock);
        }
    }

    public virtual ISupportEffect GetEffect()
    {
        return null;
    }

    public void ShakeBuilding(float intensity)
    {
        modelTransform.localPosition = new Vector3(Random.Range(-Constants.buildingShakeStrength, Constants.buildingShakeStrength) * intensity, 0, Random.Range(-Constants.buildingShakeStrength, Constants.buildingShakeStrength) * intensity);
    }
    public void UnShakeBuilding()
    {
        modelTransform.localPosition = Vector3.zero;
    }
}

public class AttackTower : Building
{
    #region Fields
    protected float rawDamage, rawFireRate, rawRange;
    public float damage { get; set; }
    public float fireRate { get; set; }
    public float range { get; set; }

    protected string projectileType;
    protected List<ISupportEffect> effects;
    protected GameObject target;
    [SerializeField] private Transform shootPointTransform;
    protected Vector3 shootOffset;

    private List<GameObject> supportSpheres;

    private float nextTimeToFire = 0f;

    // Range indication
    int rangeIndicatorsAmount = 12;
    [SerializeField] private GameObject rangeIndicator;
    List<GameObject> rangeIndicators;
    private bool showingRangeIndication;
    public bool showRangeIndication { private get; set; }
    #endregion
    protected override void OnEnable()
    {
        base.OnEnable();

        buttonData.isAttackTower = true;

        isSupport = false;

        effects = new();
        supportSpheres = new();

        color = Color.red;
        AddBlock(new Block(0, 0, this));
        shootOffset = shootPointTransform.position;

        InitializeRangeIndication();
    }

    protected virtual void Update()
    {
        if (WaveManager.Instance.waveActive)
        {
            ApplySupportEffects();

            UpdateTarget();
            if (target != null)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
                if (distanceToTarget <= range && Time.time >= nextTimeToFire)
                {
                    Vector3 directionToTarget = target.transform.position - transform.position;
                    directionToTarget.y = 0;
                    Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                    transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);

                    ShootProjectile();
                    nextTimeToFire = Time.time + 1f / fireRate;
                }
            }
        }

        showingRangeIndication = false;
        if (showRangeIndication)
        {
            showingRangeIndication = true;
            showRangeIndication = false;
        }

        if (showingRangeIndication) ShowRangeIndication();
        else HideRangeIndication();
    }

    protected void UpdateTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        GameObject closestEnemy = null;

        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag(Constants.TAG_ENEMY))
            {
                if(closestEnemy == null) closestEnemy = collider.gameObject;
                if(Vector3.Distance(transform.position, closestEnemy.transform.position) > Vector3.Distance(transform.position, collider.transform.position)) closestEnemy = collider.gameObject;
            }
        }

        if (target != closestEnemy)
        {
            target = closestEnemy;
        }
    }
    protected virtual void ShootProjectile()
    {
        GameObject projectile = ProjectilePool.Instance.GetProjectile(projectileType);

        if (projectile != null)
        {
            Vector3 targetPosition = new Vector3(target.transform.position.x, target.transform.position.y + 0.5f, target.transform.position.z);
            projectile.GetComponent<Projectile>().SetValues(transform.position + shootOffset, targetPosition, damage);
        }
    }
    public void UpdateSupportEffects()
    {
        effects.Clear();

        GameObject checkCell = PlateauManager.Instance.GetCell(blocks[0].cell.posInGrid.x + 1, blocks[0].cell.posInGrid.y);
        if (checkCell != null && checkCell.GetComponent<Cell>().buildingOnCell != null && checkCell.GetComponent<Cell>().buildingOnCell.GetEffect() != null) effects.Add(checkCell.GetComponent<Cell>().buildingOnCell.GetEffect());

        checkCell = PlateauManager.Instance.GetCell(blocks[0].cell.posInGrid.x, blocks[0].cell.posInGrid.y + 1);
        if (checkCell != null && checkCell.GetComponent<Cell>().buildingOnCell != null && checkCell.GetComponent<Cell>().buildingOnCell.GetEffect() != null) effects.Add(checkCell.GetComponent<Cell>().buildingOnCell.GetEffect());

        checkCell = PlateauManager.Instance.GetCell(blocks[0].cell.posInGrid.x - 1, blocks[0].cell.posInGrid.y);
        if (checkCell != null && checkCell.GetComponent<Cell>().buildingOnCell != null && checkCell.GetComponent<Cell>().buildingOnCell.GetEffect() != null) effects.Add(checkCell.GetComponent<Cell>().buildingOnCell.GetEffect());

        checkCell = PlateauManager.Instance.GetCell(blocks[0].cell.posInGrid.x, blocks[0].cell.posInGrid.y - 1);
        if (checkCell != null && checkCell.GetComponent<Cell>().buildingOnCell != null && checkCell.GetComponent<Cell>().buildingOnCell.GetEffect() != null) effects.Add(checkCell.GetComponent<Cell>().buildingOnCell.GetEffect());
    }
    public void ApplySupportEffects()
    {
        damage = rawDamage;
        fireRate = rawFireRate;
        range = rawRange;

        foreach (ISupportEffect effect in effects)
            effect.ApplyEffect(this);
    }
    public void PeekEffects(Cell cell)
    {
        effects.Clear();

        if (cell.buildingOnCell != null) return;

        GameObject checkCell = PlateauManager.Instance.GetCell(cell.posInGrid.x + 1, cell.posInGrid.y);

        if (checkCell != null && checkCell.GetComponent<Cell>().buildingOnCell != null && checkCell.GetComponent<Cell>().buildingOnCell.GetEffect() != null) effects.Add(checkCell.GetComponent<Cell>().buildingOnCell.GetEffect());

        checkCell = PlateauManager.Instance.GetCell(cell.posInGrid.x, cell.posInGrid.y + 1);
        if (checkCell != null && checkCell.GetComponent<Cell>().buildingOnCell != null && checkCell.GetComponent<Cell>().buildingOnCell.GetEffect() != null) effects.Add(checkCell.GetComponent<Cell>().buildingOnCell.GetEffect());

        checkCell = PlateauManager.Instance.GetCell(cell.posInGrid.x - 1, cell.posInGrid.y);
        if (checkCell != null && checkCell.GetComponent<Cell>().buildingOnCell != null && checkCell.GetComponent<Cell>().buildingOnCell.GetEffect() != null) effects.Add(checkCell.GetComponent<Cell>().buildingOnCell.GetEffect());

        checkCell = PlateauManager.Instance.GetCell(cell.posInGrid.x, cell.posInGrid.y - 1);
        if (checkCell != null && checkCell.GetComponent<Cell>().buildingOnCell != null && checkCell.GetComponent<Cell>().buildingOnCell.GetEffect() != null) effects.Add(checkCell.GetComponent<Cell>().buildingOnCell.GetEffect());
    }

    #region RangeIndication
    private void InitializeRangeIndication()
    {
        rangeIndicators = new();

        for (int i = 0; i < rangeIndicatorsAmount; i++)
        {
            GameObject cube = Instantiate(rangeIndicator);

            cube.transform.SetParent(transform, false);

            cube.SetActive(false);

            rangeIndicators.Add(cube);
        }
    }
    public void UpdateRangeIndication()
    {
        float increment = (2 * Mathf.PI) / rangeIndicatorsAmount;

        for (int i = 0; i < rangeIndicatorsAmount; i++)
        {
            rangeIndicators[i].transform.localPosition = new Vector3(Mathf.Sin(increment * i), 0, Mathf.Cos(increment * i)) * range;
            rangeIndicators[i].transform.LookAt(transform.position);
        }
    }
    private void ShowRangeIndication()
    {
        if (!rangeIndicators[0].activeSelf)
        {
            UpdateRangeIndication();
            foreach (GameObject r in rangeIndicators)
            {
                r.transform.SetParent(transform, false);
                r.SetActive(true);
            }
        }
    }
    public void HideRangeIndication()
    {
        if (rangeIndicators[0].activeSelf)
        {
            foreach (GameObject r in rangeIndicators)
            {
                r.SetActive(false);
            }
        }
    }
    #endregion
}

public class SupportBuilding : Building
{
    protected ISupportEffect effect;
    public float effectStrength { get; protected set; }

    protected override void OnEnable()
    {
        base.OnEnable();

        isSupport = true;
        buttonData.isAttackTower = false;
    }

    public override ISupportEffect GetEffect()
    {
        return effect;
    }
}