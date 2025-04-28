namespace ClubeDaLeitura.Compartilhado;

public interface IRepositorio<TRepositorio> where TRepositorio : EntidadeBase<TRepositorio>
{
    void CadastrarRegistro(TRepositorio novoRegistro);
    List<TRepositorio> PegarListaRegistrados();
    void EditarRegistro(TRepositorio registroEscolhido, TRepositorio dadosEditados);
    void ExcluirRegistro(TRepositorio registroEscolhido);
    TRepositorio SelecionarRegistroPorId(int idRegistroEscolhida);
}