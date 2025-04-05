using PrimeTween;

namespace Code.Common.Extensions
{
    public static class TweenExtensions
    {
        public static void StopIfAlive(this Tween tween)
        {
            if (tween.isAlive)
            {
                tween.Stop();
            }
        }
    }
}