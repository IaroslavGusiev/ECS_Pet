namespace Code.Gameplay.Cooldowns
{
    public static class CooldownEntityExtensions
    {
        public static GameEntity PutOnCooldown(this GameEntity entity)
        {
            if (entity.hasCooldown == false)
            {
                return entity;
            }

            entity.isCooldownUp = false;
            entity.ReplaceCooldownLeft(entity.Cooldown);
            return entity;
        }

        public static GameEntity PutOnCooldown(this GameEntity entity, float cooldown)
        {
            entity.isCooldownUp = false;
            entity.ReplaceCooldown(cooldown);
            entity.ReplaceCooldownLeft(cooldown);
            return entity;
        }
    }
}