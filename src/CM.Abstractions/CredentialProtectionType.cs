namespace CM.Abstractions
{
    /// <summary>
    /// <para>
    /// The <c>CRED_PROTECTION_TYPE</c> enumeration specifies the security context in which credentials are encrypted when using the
    /// CredProtect function.
    /// </para>
    /// </summary>
    public enum CredentialProtectionType
    {
        /// <summary>The credentials are not encrypted.</summary>
        CredUnprotected,

        /// <summary>
        /// The credentials are encrypted and can be decrypted only in the security context in which they were encrypted or in the
        /// security context of a trusted component.
        /// </summary>
        CredUserProtection,

        /// <summary>The credentials are encrypted and can only be decrypted by a trusted component.</summary>
        CredTrustedProtection,

        /// <summary/>
        CredForSystemProtection,
    }
}
