namespace CM.Abstractions
{
    /// <summary>Type of credential used by CREDENTIAL structure.</summary>
    public enum CredentialType
    {
        /// <summary>
        /// The credential is a generic credential. The credential will not be used by any particular authentication package. The
        /// credential will be stored securely but has no other significant characteristics.
        /// </summary>
        Generic = 1,

        /// <summary>
        /// The credential is a password credential and is specific to Microsoft's authentication packages. The NTLM, Kerberos, and
        /// Negotiate authentication packages will automatically use this credential when connecting to the named target.
        /// </summary>
        DomainPassword,

        /// <summary>
        /// The credential is a certificate credential and is specific to Microsoft's authentication packages. The Kerberos, Negotiate,
        /// and Schannel authentication packages automatically use this credential when connecting to the named target.
        /// </summary>
        DomainCertificate,

        /// <summary>
        /// This value is no longer supported.
        /// <para>
        /// <c>Windows Server 2003 and Windows XP:</c> The credential is a password credential and is specific to authentication packages
        /// from Microsoft. The Passport authentication package will automatically use this credential when connecting to the named target.
        /// </para>
        /// <para>
        /// Additional values will be defined in the future. Applications should be written to allow for credential types they do not understand.
        /// </para>
        /// </summary>
        DomainVisiblePassword,

        /// <summary>
        /// The credential is a certificate credential that is a generic authentication package.
        /// <para><c>Windows Server 2008, Windows Vista, Windows Server 2003 and Windows XP:</c> This value is not supported.</para>
        /// </summary>
        GenericCertificate,

        /// <summary>
        /// The credential is supported by extended Negotiate packages.
        /// <para><c>Windows Server 2008, Windows Vista, Windows Server 2003 and Windows XP:</c> This value is not supported.</para>
        /// </summary>
        DomainExtended,

        /// <summary>
        /// The maximum number of supported credential types. <note type="note">Windows Server 2008, Windows Vista, Windows Server 2003
        /// and Windows XP: This value is not supported.</note>
        /// </summary>
        Maximum,

        /// <summary>
        /// The extended maximum number of supported credential types that now allow new applications to run on older operating systems.
        /// <note type="note">Windows Server 2008, Windows Vista, Windows Server 2003 and Windows XP: This value is not supported.</note>
        /// </summary>
        MaximumEx = Maximum + 1000,
    }
}
