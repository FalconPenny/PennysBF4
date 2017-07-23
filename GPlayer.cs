using System;
using SharpDX;

namespace MultiHack
{
    class GPlayer
    {
        public string Name;
        public string VehicleName;
        public int Team;
        public Vector3 Origin;
        public Vector3 Velocity;
        public RDoll Bone;
        public int Pose;

        public Vector2 FoV;

        public Vector2 Sway;

        public int IsOccluded;
        public bool IsSpectator;

        public bool IsDriver;
        public bool InVehicle;

        public float Health;
        public float MaxHealth;

        public Gun CurrentWeapon;

        public float ShotsFired;
        public float ShotsHit;
        public float DamageCount;

        public string LastEnemyNameAimed = "";
        public DateTime LastTimeEnemyAimed = DateTime.Now;

        public Matrix ViewProj;
        public Matrix MatrixInverse;

        public bool NoBreathEnabled = false;

        public float Yaw;
        public float Distance;
        public float DistanceToCrosshair;

        // Vehicle
        public AxisAlignedBox VehicleAABB;
        public Matrix VehicleTranfsorm;
        public float VehicleHealth;
        public float VehicleMaxHealth;

        public bool IsValid()
        {
            return Health > 0.1f && Health <= 100 && !Origin.IsZero;
        }

        public bool IsValidAimbotTarget(bool bTwoSecRule, string lastTargetName, DateTime lastTimeTargeted, bool aimAtVehicles)
        {
            if (!IsValid())
            {
                return false;
            }
            bool withVehicle = aimAtVehicles ? InVehicle : !InVehicle;
            return (!bTwoSecRule || lastTargetName == Name || DateTime.Now.Subtract(lastTimeTargeted).Seconds >= 2) && withVehicle;
        }

        public bool IsDead()
        {
            return Health < 0.1f;
        }

        public bool IsVisible()
        {
            return IsOccluded == 0;
        }

        public bool IsSprinting()
        {
            return ((float) Math.Abs(Velocity.X + Velocity.Y + Velocity.Z)) > 4.0f;
        }

        public float GetShotsAccuracy()
        {
            if ((ShotsFired > 0f && ShotsHit > 0f))
                return (float)Math.Round((double)((float)((ShotsHit / ShotsFired) * 100.0f)), 2);
            return 0.0f;
        }

        public AxisAlignedBox GetAABB()
        {
            AxisAlignedBox aabb = new AxisAlignedBox();
            if (this.Pose == Poses.STANDING) // standing
            {
                aabb.Min = new Vector4(-0.350000f, 0.000000f, -0.350000f, 0);
                aabb.Max = new Vector4(0.350000f, 1.700000f, 0.350000f, 0);
            }
            else if (this.Pose == Poses.CROUCHING) // crouching
            {
                aabb.Min = new Vector4(-0.350000f, 0.000000f, -0.350000f, 0);
                aabb.Max = new Vector4(0.350000f, 1.150000f, 0.350000f, 0);
            }
            else if (this.Pose == Poses.PRONE) // prone
            {
                aabb.Min = new Vector4(-0.350000f, 0.000000f, -0.350000f, 0);
                aabb.Max = new Vector4(0.350000f, 0.400000f, 0.350000f, 0);
            }
            return aabb;
        }
    }
}
