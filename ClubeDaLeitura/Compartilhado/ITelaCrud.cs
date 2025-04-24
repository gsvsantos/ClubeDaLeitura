namespace ClubeDaLeitura.Compartilhado;

public interface ITelaCrud
{
    string ApresentarMenu();
    void CadastrarRegistro();
    void MostrarListaRegistrados(bool exibirCabecalho, bool comId);
    void EditarRegistro();
    void ExcluirRegistro();
}
