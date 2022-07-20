namespace BunkerGame.GameTypes.CharacterTypes
{
    public enum MethodType
    {
        None,
        /// <summary>
        /// self
        /// </summary>
        Update,
        /// <summary>
        /// target
        /// </summary>
        Change,
        Add,
        Exchange,
        Remove,
        PassiveRemove,
        /// <summary>
        /// target
        /// </summary>
        Spy,
        /// <summary>
        /// on yourself
        /// </summary>
        SpyYourself
    }
}
