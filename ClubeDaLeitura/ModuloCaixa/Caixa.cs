using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloRevista;

namespace ClubeDaLeitura.ModuloCaixa;

public class Caixa : EntidadeBase
{
    public string Etiqueta;
    public int Cor;
    public int DiasEmprestimo;
    public Revista[] Revistas = new Revista[40];

    public Caixa(string etiqueta, int cor, int diasEmprestimo)
    {
        Etiqueta = etiqueta;
        Cor = cor;
        DiasEmprestimo = diasEmprestimo;
    }
    public override string Validar()
    {
        string erros = "";

        if (string.IsNullOrWhiteSpace(Etiqueta))
            erros += "\nO campo 'Nome' é obrigatório.\n";
        else
        {
            if (!Etiqueta.All(char.IsLetter))
                erros += "O campo 'Etiqueta' precisa conter apenas letras.\n";

            if (Etiqueta.Length > 50)
                erros += "O campo 'Etiqueta' não pode ter mais que 50 caracteres.\n";
        }

        if (string.IsNullOrWhiteSpace(Cor.ToString()))
            erros += "O campo 'Cor' é obrigatório.\n";
        else
        {
            int[] enumConsoleColor = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];
            if (!enumConsoleColor.Contains(Cor))
                erros += "O campo 'Cor' precisa ser uma das cores da tabela!\n";
        }

        if (string.IsNullOrEmpty(DiasEmprestimo.ToString()))
            erros += "O campo 'Dias de Emprestimo' é obrigatório.\n";
        else
            if (DiasEmprestimo != 3 && DiasEmprestimo != 7)
            erros += "O campo 'Dias de Emprestimo' está inválido! Verifique novamente a raridade da revista!";

        return erros;
    }
    public void AdicionarRevista(Revista novaRevista)
    {
        for (int i = 0; i < Revistas.Length; i++)
        {
            if (Revistas[i] == null)
            {
                Revistas[i] = novaRevista;
                return;
            }
        }
    }
    public void RemoverRevista(Revista revistaEscolhida)
    {
        for (int i = 0; i < Revistas.Length; i++)
        {
            if (Revistas[i] == null)
                continue;

            if (Revistas[i] == revistaEscolhida)
            {
                Revistas[i] = null!;
                return;
            }
        }
    }
    public override void AtualizarRegistro(EntidadeBase dadosEditados)
    {
        Caixa caixaEditada = (Caixa)dadosEditados;

        Etiqueta = caixaEditada.Etiqueta;
        Cor = caixaEditada.Cor;
        DiasEmprestimo = caixaEditada.DiasEmprestimo;
    }
}
