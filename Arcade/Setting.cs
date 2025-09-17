namespace Arcade
{
    public static class Setting
    {
        public static float ForwardAccelerator => 12.0f;
        public static float ReverseAccelerator => 6.0f;
        public static float MaxSpeed => 60.0f;
        public static float TurnStrength => 180.0f;
        public static float GravityForce => -6.0f;
        public static float GroundRayLength => 0.41f;
        public static float RayCastWait => 0.2f;
        public static int EdgeMistake => 6;
        public static int GroundMistake => 2;
        public static float DragGround => 3.0f;
        public static float DragAir => 0.2f;

#if UNITY_EDITOR
        public static float JumpForce => 12f;
#else
        public static float JumpForce => 24f;
#endif
        
    }
}