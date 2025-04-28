namespace ClubeDaLeitura.Compartilhado;

public abstract class RepositorioBaseEmMemoria<TRepositorio> where TRepositorio : EntidadeBase<TRepositorio> // boxing
{
    public List<TRepositorio> Registros { get; protected set; } = new List<TRepositorio>();
    private int id = 0;
    public bool ListaVazia { get; set; } = false;

    public virtual void CadastrarRegistro(TRepositorio novoRegistro)
    {
        novoRegistro.Id = ++id;
        Registros.Add(novoRegistro);
    }
    public List<TRepositorio> PegarListaRegistrados()
    {
        return Registros;
    }
    public void EditarRegistro(TRepositorio registroEscolhido, TRepositorio dadosEditados)
    {
        registroEscolhido.AtualizarRegistro(dadosEditados);
    }
    public void ExcluirRegistro(TRepositorio registroEscolhido)
    {
        foreach (TRepositorio registro in Registros)
        {
            if (registroEscolhido == registro)
            {
                Registros.Remove(registroEscolhido);
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
}
