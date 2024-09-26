
using Unity.VisualScripting;
using UnityEngine;

namespace CodeBase.Data
{
    public static class DataExtensions
    {
        public static Vector3Data AsVectorData(this Vector3 vector3)
        {
            return new Vector3Data(vector3.x, vector3.y , vector3.z);
        }
    }
}
