using UnityEngine;

namespace DefaultNamespace
{
    public class Obstacle : MonoBehaviour
    {
        private void Start()
        {
            RegisterObstacle();
        }

        private void RegisterObstacle()
        {
            Vector3 pos = transform.position;
            int i = (int) ((pos.y - 4.75) / -0.5);
            int j = (int)((pos.x + 2.25) / 0.5);
            TetrisBehavior.Instance.board[i, j] = 1;
        }
    }
}