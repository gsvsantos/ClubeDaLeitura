namespace ClubeDaLeitura.Compartilhado;

public interface IRepositorio<TRepositorio> where TRepositorio : EntidadeBase<TRepositorio>
{
    bool ListaVazia { get; set; }

    void CadastrarRegistro(TRepositorio novoRegistro);
    List<TRepositorio> PegarListaRegistrados();
    void EditarRegistro(TRepositorio registroEscolhido, TRepositorio dadosEditados);
    void ExcluirRegistro(TRepositorio registroEscolhido);
    TRepositorio SelecionarRegistroPorId(int idRegistroEscolhida);
}