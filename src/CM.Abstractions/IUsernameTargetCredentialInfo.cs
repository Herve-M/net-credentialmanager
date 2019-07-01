namespace CM
{
    /// <summary>
    /// The <c>USERNAME_TARGET_CREDENTIAL_INFO</c> structure contains a reference to a credential. This structure is used to pass a user
    /// name into the CredMarshalCredential function and out of the CredUnmarshalCredential. The resultant marshaled credential can be
    /// passed as the lpszUserName parameter of the LogonUser function to direct that API to get the password from the corresponding
    /// CRED_FLAGS_USERNAME_TARGET credential instead of from the lpszPassword parameter of the function.
    /// </summary>
    public interface IUsernameTargetCredentialInfo
    {
        /// <summary>
        /// <para>User name of the USERNAME_TARGET_CREDENTIAL_INFO credential.</para>
        /// </summary>
        string UserName { get; set; }
    }
}