using UnityEngine;
using UnityEngine.Splines;

namespace Shmup
{
    public class EnemyFactory
    {
        public GameObject CreateEnemy(EnemyType enemyType, SplineContainer spline)
        {
            EnemyBuilder builder = new EnemyBuilder()
                .SetBasePrefab(enemyType.enemyPrefab)
                .SetSpline(spline)
                .SetSpeed(enemyType.speed);

            return builder.Build();
        }

    }
}