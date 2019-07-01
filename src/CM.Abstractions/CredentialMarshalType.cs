namespace CM.Abstractions
{
    /// <summary>
    /// <para>
    /// The <c>CRED_MARSHAL_TYPE</c> enumeration specifies the types of credential to be marshaled by CredMarshalCredential or
    /// unmarshaled by CredUnmarshalCredential.
    /// </para>
    /// </summary>
    public enum CredentialMarshalType
    {
        /// <summary>Specifies that the credential is a certificate reference described by a CERT_CREDENTIAL_INFO structure.</summary>
        CertCredential = 1,

        /// <summary>
        /// Specifies that the credential is a reference to a CRED_FLAGS_USERNAME_TARGET credential described by a
        /// USERNAME_TARGET_CREDENTIAL_INFO structure.
        /// </summary>
        UsernameTargetCredential,

        /// <summary>Undocumented.</summary>
        BinaryBlobCredential,

        UsernameForPackedCredentials,
        BinaryBlobForSystem
    }
}
