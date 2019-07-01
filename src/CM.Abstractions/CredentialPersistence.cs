namespace CM.Abstractions
{
    /// <summary>
    /// Specifies the maximum persistence supported by the corresponding credential type.
    /// </summary>
    public enum CredentialPersistence : uint
    {
        /// <summary>
        /// No credential can be stored. This value will be returned if the credential type is not supported or has been disabled by policy.
        /// </summary>
        None,

        /// <summary>
        /// The credential persists for the life of the logon session. It will not be visible to other logon sessions of this same user.
        /// It will not exist after this user logs off and back on.
        /// </summary>
        Session,

        /// <summary>
        /// The credential persists for all subsequent logon sessions on this same computer. It is visible to other logon sessions of
        /// this same user on this same computer and not visible to logon sessions for this user on other computers.
        /// <para>
        /// Windows Vista Home Basic, Windows Vista Home Premium, Windows Vista Starter and Windows XP Home Edition: This value is not supported.
        /// </para>
        /// </summary>
        LocalMachine,

        /// <summary>
        /// The credential persists for all subsequent logon sessions on this same computer. It is visible to other logon sessions of
        /// this same user on this same computer and to logon sessions for this user on other computers.
        /// <para>
        /// This option can be implemented as locally persisted credential if the administrator or user configures the user account to
        /// not have roam-able state. For instance, if the user has no roaming profile, the credential will only persist locally.
        /// </para>
        /// <para>
        /// Windows Vista Home Basic, Windows Vista Home Premium, Windows Vista Starter and Windows XP Home Edition: This value is not supported.
        /// </para>
        /// </summary>
        Enterprise,
    }
}
