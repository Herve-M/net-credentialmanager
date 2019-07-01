using CM.Abstractions;
using System;
using System.Diagnostics;
using Vanara.Extensions;
using Vanara.InteropServices;
using Vanara.PInvoke;

namespace CM
{
    [DebuggerDisplay("[{Type}] {TargetName}")]
    public class Credential : ICredential
    {
        public CredentialType Type { get; set; }
        public CredentialFlags Flags { get; set; }
        public CredentialPersistence Persistence { get; set; }
        public string TargetName { get; set; }
        public string TargetAlias { get; set; }
        public string UserName { get; set; }
        public string Comment { get; set; }
        public DateTime LastWritten { get; }
        public byte[] Attributes { get; set; }
        public uint AttributeCount => (uint)(Attributes?.Length ?? default);
        public byte[] CredentialBlob { get; set; }
        public uint CredentialBlobSize => (uint)(CredentialBlob?.Length ?? default);

        public Credential(CredentialType type, CredentialPersistence persistence, string targetName, string userName)
        {
            Type = type;
            Persistence = persistence;
            TargetName = targetName;
            UserName = userName;
        }

        public TDestination GetPassword<TDestination>(ICredentialBlobConverter<TDestination> converter)
        {
            return converter.Convert(CredentialBlob);
        }

        protected internal Credential(in AdvApi32.CREDENTIAL_MGD credentialMgd)
        {
            Flags = (CredentialFlags)credentialMgd.Flags;
            Type = (CredentialType)credentialMgd.Type;
            TargetName = credentialMgd.TargetName;
            Comment = credentialMgd.Comment;
            LastWritten = credentialMgd.LastWritten.ToDateTime();
            CredentialBlob = credentialMgd.CredentialBlob;
            Persistence = (CredentialPersistence)credentialMgd.Persist;
            Attributes = credentialMgd.Attributes;
            TargetAlias = credentialMgd.TargetAlias;
            UserName = credentialMgd.UserName;
        }

        protected internal Credential(in AdvApi32.CREDENTIAL credential)
        {
            Flags = (CredentialFlags)credential.Flags;
            Type = (CredentialType)credential.Type;
            TargetName = credential.TargetName;
            Comment = credential.Comment;
            LastWritten = credential.LastWritten.ToDateTime();
            CredentialBlob = credential.CredentialBlob.ToArray<byte>((int)credential.CredentialBlobSize);
            Persistence = (CredentialPersistence)credential.Persist;
            Attributes = credential.Attributes.ToArray<byte>((int)credential.AttributeCount);
            TargetAlias = credential.TargetAlias;
            UserName = credential.UserName;
        }

        //TODO: see Enum with Flag cast??
        public static implicit operator AdvApi32.CREDENTIAL(Credential credential)
        {
            var uPCredentialBlob = new SafeHGlobalHandle(credential.CredentialBlob);
            var uPAttributes = credential.Attributes == null ? IntPtr.Zero : new SafeHGlobalHandle(credential.Attributes);

            var nCredential = new AdvApi32.CREDENTIAL
            {
                Flags = (AdvApi32.CRED_FLAGS)credential.Flags,
                Type = (AdvApi32.CRED_TYPE)credential.Type,
                TargetName = new StrPtrAuto(credential.TargetName),
                Comment = string.IsNullOrEmpty(credential.Comment) ? new StrPtrAuto() : new StrPtrAuto(credential.Comment),
                CredentialBlob = uPCredentialBlob.DangerousGetHandle(),
                CredentialBlobSize = credential.CredentialBlobSize,
                Persist = (AdvApi32.CRED_PERSIST)credential.Persistence,
                AttributeCount = (uint)credential.AttributeCount,
                Attributes = uPAttributes.DangerousGetHandle(),
                TargetAlias = new StrPtrAuto(credential.TargetAlias),
                UserName = new StrPtrAuto(credential.UserName)
            };
            
            return nCredential;
        }

        public override string ToString()
        {
            return $"CredentialType: {Type}, TargetName: {TargetName}, UserName: {UserName}, Comment: {Comment}";
        }
    }
}
