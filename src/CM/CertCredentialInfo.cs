using CM.Abstractions;

namespace CM
{
    public sealed class CertCredentialInfo : BaseInfoClass
    {
        /// <summary>
        /// <para>
        /// Size of the structure in bytes. This member should be set to . This structure might be a larger value in the future,
        /// indicating a newer version of the structure.
        /// </para>
        /// </summary>
        public uint cbSize;

        /// <summary>
        /// <para>SHA-1 hash of the certificate referenced.</para>
        /// </summary>
        public byte[] rgbHashOfCert;
    }
}
