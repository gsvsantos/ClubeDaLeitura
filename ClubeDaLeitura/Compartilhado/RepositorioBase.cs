namespace ClubeDaLeitura.Compartilhado;

public abstract class RepositorioBase<T> where T : EntidadeBase<T> // boxing
{
    public List<T> Registros { get; protected set; } = new List<T>();
    private int id = 0;
    public bool ListaVazia { get; set; } = false;

    public virtual void CadastrarRegistro(T novoRegistro)
    {
        novoRegistro.Id = ++id;
        Registros.Add(novoRegistro);
    }
    public List<T> PegarListaRegistrados()
    {
        return Registros;
    }
    public void EditarRegistro(T registroEscolhido, T dadosEditados)
    {
        registroEscolhido.AtualizarRegistro(dadosEditados);
    }
    public void ExcluirRegistro(T registroEscolhido)
    {
        foreach (T registro in Registros)
        {
            if (registroEscolhido == registro)
                Registros.Remove(registroEscolhido);
        }
    }
    public T SelecionarRegistroPorId(int idRegistroEscolhida)
    {
        foreach (T e in Registros)
        {
            if (e.Id == idRegistroEscolhida)
                return e;
        }

        return null!;
    }
}
