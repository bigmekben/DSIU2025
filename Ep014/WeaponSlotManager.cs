using UnityEngine;
// with notes and ideas from Ben
namespace SG
{
    public class WeaponSlotManager : MonoBehaviour
    {
        WeaponHolderSlot leftHandSlot;
        WeaponHolderSlot rightHandSlot;

        DamageCollider leftHandDamageCollider;
        DamageCollider rightHandDamageCollider;

        Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();

            // This will look in the bones of the model's skeleton, which count as game objects:
            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
            {
                if(weaponSlot.isLeftHandSlot)
                {
                    leftHandSlot = weaponSlot;
                }
                else if (weaponSlot.isRightHandSlot)
                {
                    rightHandSlot = weaponSlot;
                }
            }
            // Tutorial assumes weapon slots only placed on left and right hands.
            // Different behavior if other weapon slots placed, for example, on belt, back, or ankle.
            // Alternative to above loop:
            // using System.Linq;  // add to top of class
            // ....
            //leftHandSlot = weaponHolderSlots.FirstOrDefault(slot => slot.isLeftHandSlot);
            //rightHandSlot = weaponHolderSlots.FirstOrDefault(slot => slot.isRightHandSlot);
            // if looking for a back slot, ankle slot, etc. could just filter on a different property.
            // or instead of having one property per slot location, use an enum.
        }

        public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
        {
            if(isLeft)
            {
                leftHandSlot.LoadWeaponModel(weaponItem);
                LoadLeftWeaponDamageCollider();
                #region Handle Left Weapon Idle Animations
                if (weaponItem != null)
                {
                    animator.CrossFade(weaponItem.left_Hand_Idle, 0.2f);
                }
                else
                {
                    animator.CrossFade("Left Arm Empty", 0.2f);
                }
                #endregion
            }
            else
            {
                rightHandSlot.LoadWeaponModel(weaponItem);
                LoadRightWeaponDamageCollider();
                #region Handle Right Weapon Idle Animations
                if (weaponItem != null)
                {
                    animator.CrossFade(weaponItem.right_Hand_Idle, 0.2f);
                }
                else
                {
                    animator.CrossFade("Right Arm Empty", 0.2f);
                }
                #endregion
            }
        }

        #region Handle Weapon's Damage Colliders

        private void LoadLeftWeaponDamageCollider()
        {
            leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        }

        private void LoadRightWeaponDamageCollider()
        {
            rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        }

        public void OpenRightHandDamageCollider()
        {
            rightHandDamageCollider.EnableDamageCollider();
        }

        public void OpenLeftHandDamageCollider()
        {
            leftHandDamageCollider.EnableDamageCollider();
        }

        public void CloseRightHandDamageCollider()
        {
            rightHandDamageCollider.DisableDamageCollider();
        }

        public void CloseLeftHandDamageCollider()
        {
            leftHandDamageCollider.DisableDamageCollider();
        }

        #endregion

    }
}
