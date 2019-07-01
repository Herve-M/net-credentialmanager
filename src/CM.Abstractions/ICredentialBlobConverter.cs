namespace CM.Abstractions
{
    public interface ICredentialBlobConverter<out TDestination>
    {
        TDestination Convert(byte[] source /*, Context context*/);
    }
}
