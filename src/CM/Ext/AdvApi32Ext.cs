using System;
using System.Collections.Generic;
using System.Linq;
using Vanara.Extensions;
using Vanara.InteropServices;
using Vanara.PInvoke;

namespace CM.Ext
{
    public static class AdvApi32Ext
    {
        public static AdvApi32.CREDENTIAL ToNativeCredential(this AdvApi32.CREDENTIAL_MGD mgd)
        {
            var uPCredentialBlob = new SafeHGlobalHandle(mgd.CredentialBlob);
            var uPAttributes = mgd.Attributes == null ? IntPtr.Zero : new SafeHGlobalHandle(mgd.Attributes);

            var nCredential = new AdvApi32.CREDENTIAL
            {
                Flags = mgd.Flags,
                Type = mgd.Type,
                TargetName = new StrPtrAuto(mgd.TargetName),
                Comment = string.IsNullOrEmpty(mgd.Comment) ? new StrPtrAuto() : new StrPtrAuto(mgd.Comment),
                LastWritten = mgd.LastWritten,
                CredentialBlob = uPCredentialBlob.DangerousGetHandle(),
                Persist = mgd.Persist,
                AttributeCount = mgd.AttributeCount,
                Attributes = uPAttributes.DangerousGetHandle(),
                TargetAlias = new StrPtrAuto(mgd.TargetAlias),
                UserName = new StrPtrAuto(mgd.UserName)
            };

            return nCredential;
        }

        public static AdvApi32.CREDENTIAL_MGD ToCredentialMGD(this AdvApi32.CREDENTIAL c)
        {
            return new AdvApi32.CREDENTIAL_MGD()
            {
                Flags = c.Flags,
                Type = c.Type,
                TargetName = c.TargetName,
                Comment = c.Comment,
                LastWritten = c.LastWritten,
                CredentialBlob = c.CredentialBlob.ToArray<byte>((int)c.CredentialBlobSize),
                Persist = c.Persist,
                AttributeCount = c.AttributeCount,
                Attributes = c.Attributes.ToArray<byte>((int)c.AttributeCount),
                TargetAlias = c.TargetAlias,
                UserName = c.UserName
            };
        }

        public static T GetNativeStructure<T>(this AdvApi32.SafeCredMemoryHandle handle) where T: struct
        {
            return handle.DangerousGetHandle().ToStructure<T>();
        }

        public static IEnumerable<T> GetNativeArrayStructure<T>(this AdvApi32.SafeCredMemoryHandle handle, int count) where T : struct
        {
            return handle.DangerousGetHandle().ToIEnum<IntPtr>(count).Select(p => p.ToStructure<T>()).ToArray();
        }
    }
}
