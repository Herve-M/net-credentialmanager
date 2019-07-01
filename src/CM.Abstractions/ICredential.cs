using System;

namespace CM.Abstractions
{
    /// <summary>The CREDENTIAL structure contains an individual credential.</summary>
    public interface ICredential
    {
        /// <summary>
        /// The number of application-defined attributes to be associated with the credential. This member can be read and written. Its
        /// value cannot be greater than CRED_MAX_ATTRIBUTES (64).
        /// </summary>
        uint AttributeCount { get; }

        /// <summary>Application-defined attributes that are associated with the credential. This member can be read and written.</summary>
        byte[] Attributes { get; set; }

        /// <summary>
        /// A string comment from the user that describes this credential. This member cannot be longer than CRED_MAX_STRING_LENGTH (256) characters.
        /// </summary>
        string Comment { get; set; }

        /// <summary>
        /// Secret data for the credential. The CredentialBlob member can be both read and written.
        /// <para>
        /// If the Type member is CRED_TYPE_DOMAIN_PASSWORD, this member contains the plaintext Unicode password for UserName. The
        /// CredentialBlob and CredentialBlobSize members do not include a trailing zero character. Also, for CRED_TYPE_DOMAIN_PASSWORD,
        /// this member can only be read by the authentication packages.
        /// </para>
        /// <para>
        /// If the Type member is CRED_TYPE_DOMAIN_CERTIFICATE, this member contains the clear test Unicode PIN for UserName. The
        /// CredentialBlob and CredentialBlobSize members do not include a trailing zero character. Also, this member can only be read by
        /// the authentication packages.
        /// </para>
        /// <para>If the Type member is CRED_TYPE_GENERIC, this member is defined by the application.</para>
        /// <para>
        /// Credentials are expected to be portable. Applications should ensure that the data in CredentialBlob is portable. The
        /// application defines the byte-endian and alignment of the data in CredentialBlob.
        /// </para>
        /// </summary>
        byte[] CredentialBlob { get; set; }

        /// <summary>
        /// The size, in bytes, of the CredentialBlob member. This member cannot be larger than CRED_MAX_CREDENTIAL_BLOB_SIZE (512) bytes.
        /// </summary>
        uint CredentialBlobSize { get; }

        /// <summary>The flags</summary>
        CredentialFlags Flags { get; set; }

        /// <summary>
        /// The time, in Coordinated Universal Time (Greenwich Mean Time), of the last modification of the credential.
        /// </summary>
        DateTime LastWritten { get; }

        /// <summary>
        /// Defines the persistence of this credential. This member can be read and written.
        /// <list type="table">
        /// <listheader>
        /// <term>Value</term>
        /// <term>Meaning</term>
        /// </summary>
        CredentialPersistence Persistence { get; set; }

        /// <summary>
        /// Alias for the TargetName member. This member can be read and written. It cannot be longer than CRED_MAX_STRING_LENGTH (256) characters.
        /// <para>If the credential Type is CRED_TYPE_GENERIC, this member can be non-NULL, but the credential manager ignores the member.</para>
        /// </summary>
        string TargetAlias { get; set; }

        /// <summary>
        /// The name of the credential. The TargetName and Type members uniquely identify the credential. This member cannot be changed
        /// after the credential is created. Instead, the credential with the old name should be deleted and the credential with the new
        /// name created.
        /// <para>
        /// If Type is CRED_TYPE_DOMAIN_PASSWORD or CRED_TYPE_DOMAIN_CERTIFICATE, this member identifies the server or servers that the
        /// credential is to be used for. The member is either a NetBIOS or DNS server name, a DNS host name suffix that contains a
        /// wildcard character, a NetBIOS or DNS domain name that contains a wildcard character sequence, or an asterisk.
        /// </para>
        /// <para>If TargetName is a DNS host name, the TargetAlias member can be the NetBIOS name of the host.</para>
        /// <para>
        /// If the TargetName is a DNS host name suffix that contains a wildcard character, the leftmost label of the DNS host name is an
        /// asterisk (*), which denotes that the target name is any server whose name ends in the specified name, for example, *.microsoft.com.
        /// </para>
        /// <para>
        /// If the TargetName is a domain name that contains a wildcard character sequence, the syntax is the domain name followed by a
        /// backslash and asterisk (\*), which denotes that the target name is any server that is a member of the named domain (or realm).
        /// </para>
        /// <para>
        /// If TargetName is a DNS domain name that contains a wildcard character sequence, the TargetAlias member can be a NetBIOS
        /// domain name that uses a wildcard sequence for the same domain.
        /// </para>
        /// <para>
        /// If TargetName specifies a DFS share, for example, DfsRoot\DfsShare, then this credential matches the specific DFS share and
        /// any servers reached through that DFS share.
        /// </para>
        /// <para>If TargetName is a single asterisk (*), this credential matches any server name.</para>
        /// <para>
        /// If TargetName is CRED_SESSION_WILDCARD_NAME, this credential matches any server name. This credential matches before a single
        /// asterisk and is only valid if Persist is CRED_PERSIST_SESSION. The credential can be set by applications that want to
        /// temporarily override the default credential.
        /// </para>
        /// <para>This member cannot be longer than CRED_MAX_DOMAIN_TARGET_NAME_LENGTH (337) characters.</para>
        /// <para>
        /// If the Type is CRED_TYPE_GENERIC, this member should identify the service that uses the credential in addition to the actual
        /// target. Microsoft suggests the name be prefixed by the name of the company implementing the service. Microsoft will use the
        /// prefix "Microsoft". Services written by Microsoft should append their service name, for example Microsoft_RAS_TargetName.
        /// This member cannot be longer than CRED_MAX_GENERIC_TARGET_NAME_LENGTH (32767) characters.
        /// </para>
        /// <para>This member is case-insensitive.</para>
        /// </summary>
        string TargetName { get; set; }

        /// <summary>The type of the credential. This member cannot be changed after the credential is created.</summary>
        CredentialType Type { get; set; }

        /// <summary>
        /// The user name of the account used to connect to TargetName.
        /// <para>If the credential Type is CRED_TYPE_DOMAIN_PASSWORD, this member can be either a DomainName\UserName or a UPN.</para>
        /// <para>
        /// If the credential Type is CRED_TYPE_DOMAIN_CERTIFICATE, this member must be a marshaled certificate reference created by
        /// calling CredMarshalCredential with a CertCredential.
        /// </para>
        /// <para>If the credential Type is CRED_TYPE_GENERIC, this member can be non-NULL, but the credential manager ignores the member.</para>
        /// <para>This member cannot be longer than CRED_MAX_USERNAME_LENGTH (513) characters.</para>
        /// </summary>
        string UserName { get; set; }
    }
}