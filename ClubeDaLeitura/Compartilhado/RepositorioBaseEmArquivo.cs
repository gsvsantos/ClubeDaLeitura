namespace ClubeDaLeitura.Compartilhado;

public abstract class RepositorioBaseEmArquivo<TRepositorio> where TRepositorio : EntidadeBase<TRepositorio>
{
    public bool ListaVazia { get; set; } = false;
    protected ContextoDados Contexto;
    public List<TRepositorio> Registros { get; set; } = new List<TRepositorio>();
    private int id = 0;

    protected RepositorioBaseEmArquivo(ContextoDados contexto)
    {
        Contexto = contexto;
        Registros = ObterListaContexto();

        int maiorId = 0;

        foreach (TRepositorio t in Registros)
        {
            if (t is null)
                continue;

            if (t.Id > maiorId)
                maiorId = t.Id;
        }

        id = maiorId;
    }
    public virtual void CadastrarRegistro(TRepositorio novoRegistro)
    {
        novoRegistro.Id = ++id;
        Registros.Add(novoRegistro);
        Contexto.SalvarContexto();
    }
    public List<TRepositorio> PegarListaRegistrados()
    {
        return Registros;
    }
    public void EditarRegistro(TRepositorio registroEscolhido, TRepositorio dadosEditados)
    {
        registroEscolhido.AtualizarRegistro(dadosEditados);
        Contexto.SalvarContexto();
    }
    public void ExcluirRegistro(TRepositorio registroEscolhido)
    {
        foreach (TRepositorio registro in Registros)
        {
            if (registroEscolhido == registro)
            {
                Registros.Remove(registroEscolhido);
                Contexto.SalvarContexto();
                return;
            }
        }
    }
    public TRepositorio SelecionarRegistroPorId(int idRegistroEscolhida)
    {
        foreach (TRepositorio e in Registros)
        {
            if (e.Id == idRegistroEscolhida)
                return e;
        }

        return null!;
    }
    protected abstract List<TRepositorio> ObterListaContexto();
}
