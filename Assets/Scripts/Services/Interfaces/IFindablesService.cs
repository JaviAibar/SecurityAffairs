public interface IFindablesService
{
    void ResetFindables();
    void SelectableFound();

    int Founds { get; }
}