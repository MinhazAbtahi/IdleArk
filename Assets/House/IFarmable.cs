public interface IFarmable 
{
    bool IsFarmable { get; set; }

    int Farm();
}
