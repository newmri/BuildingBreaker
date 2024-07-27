using UnityEngine;
using UnityCoreLibrary.Animation;

namespace UnityCoreLibrary.EFfect
{
    public class Effect : MonoBehaviour
    {
        private Animator _animator;

        void Start()
        {
            _animator = GetComponent<Animator>();

            _animator.SetBool("Run", true, (string name) =>
            {
                _animator.SetBool(name, false);
                Destroy(gameObject, 0);
            });
        }
    }
}
