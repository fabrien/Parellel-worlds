using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public GameObject world1;
    public GameObject world2;
    private Grid _grid1;
    private Grid _grid2;

    public bool allowMovement;
    public Vector2Int direction;

    public float timer;
    public int _xInput;
    private int _yInput;
    
    private Grid _visibleGrid;
    private Grid _invisibleGrid;

    private int _visibilityCount;
    public int visibilityTrigger = 3;

    public bool doorLocked = true;
    
    // Start is called before the first frame update
    private void Start()
    {
        _grid1 = world1.GetComponent<Grid>();
        _grid2 = world2.GetComponent<Grid>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        HandleInput();
        if (timer < Settings.DeltaTime) return;
        HandleVisibility();
        _grid1.DeltaUpdate(direction);
        _grid2.DeltaUpdate(direction);
        ResetValues();
    }

    private void ResetValues()
    {
        _xInput = 0;
        _yInput = 0;
        timer = 0;
    }

    private void HandleVisibility()
    {
        if (_xInput != 0 || _yInput != 0) {
            ++_visibilityCount;
            if (!_visibleGrid && !_invisibleGrid) {
                _visibleGrid = _grid1;
                _invisibleGrid = _grid2;
                _invisibleGrid.TriggerVisibility();
            }
        }

        if (_visibilityCount >= visibilityTrigger) {
            _visibleGrid.TriggerVisibility();
            _invisibleGrid.TriggerVisibility();
            var tmp = _visibleGrid;
            _visibleGrid = _invisibleGrid;
            _invisibleGrid = tmp;
            _visibilityCount = 0;
        }
    }

    private void HandleInput()
    {
        UserInput.Update();

        if (_xInput == 0)
            _xInput = UserInput.Input.x;

        if (_yInput == 0)
            _yInput = UserInput.Input.y;

        direction.x = _xInput;
        direction.y = _yInput;
        allowMovement = AllowMovement(direction);
    }

    private bool AllowMovement(Vector2Int direction)
    {
        return !_grid1.WouldCollide(direction) && !_grid2.WouldCollide(direction);
    }

    public void CompleteLevel()
    {
        Debug.Log("YOU FINISHED THE LEVEL!");
        _invisibleGrid.TriggerVisibility();
        _visibleGrid = null;
        _invisibleGrid = null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
