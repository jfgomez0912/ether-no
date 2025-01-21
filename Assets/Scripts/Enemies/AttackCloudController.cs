using UnityEngine;

namespace Enemies
{
    public class CloudController : MonoBehaviour
    {
        private void DestroyCloud()
        {
            Destroy(this.gameObject);
        }
    
    }
}
