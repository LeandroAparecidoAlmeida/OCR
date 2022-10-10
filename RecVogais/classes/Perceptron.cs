using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using AForge;
using AForge.Neuro;
using AForge.Neuro.Learning;

namespace RecVogais {

    /// <summary>
    /// Classe que implementa um Perceptron Simples utilizando o framework AForge. A
    /// função desse Perceptron é fazer a classificação de vogais manuscritas desenhadas
    /// na tela do programa.
    /// </summary>
    public class Perceptron {

        /// <summary>
        /// Indica se a rede neural artificial está treinada ou não.
        /// </summary>
        private bool treinada = false;
        /// <summary>
        /// Rede neural artificial responsável pelo reconhecimento das vogais manuscritas.
        /// É uma implementação do framework AForge.
        /// </summary>
        private ActivationNetwork network;

        /// <summary>
        /// Constructor da classe. Carrega o aprendizado da rede neural gravado no arquivo
        /// "aprendizado.dat".
        /// </summary>
        public Perceptron() {
            CarregarAprendizado();
        }

        /// <summary>
        /// Carregar o objeto de ActivationNetwork gravado no arquivo "aprendizado.dat"
        /// no diretório raiz. Este objeto é o aprendizado da rede neural, que a habilita
        /// para o reconhecimento de vogais manuscritas.
        /// </summary>
        private void CarregarAprendizado() {
            try {            
                String arquivoAprendizado = @Application.StartupPath + @"\aprendizado.dat";
                if (File.Exists(arquivoAprendizado)) {
                    network = (ActivationNetwork) ActivationNetwork.Load(arquivoAprendizado);
                    treinada = true;
                }
            } catch (Exception ex) {
                treinada = false;
            }
        }

        /// <summary>
        /// Gerar uma string com caracteres alfanuméricos aleatórios para compôr o 
        /// nome de um arquivo de amostra. O nome de um arquivo tem um total de 10
        /// caracteres.
        /// </summary>
        /// <param name="diretorio">diretório aonde o arquivo será salvo.</param>
        /// <param name="extensao">extensão do arquivo (ex.: .jpeg, .bmp).</param>
        /// <returns>path do arquivo.</returns>

        private String ObterNomeArquivo(String diretorio, String extensao) {
            char[] caracteres = new char[] {'A', 'B', 'C', 'D', 'E', 'F',
            'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R',
            'S', 'T', 'U', 'V', 'X', 'W', 'Y', 'Z', '0', '1', '2', '3', '4',
            '5', '6', '7', '8', '9'};
            Random rnd = new Random();
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= 10; i++) {
                int idx = rnd.Next(caracteres.Length);
                sb.Append(caracteres[idx]);
            }
            return diretorio + @"\" + sb.ToString() + extensao;
        }
 
        /// <summary>
        /// Salvar desenho no banco de dados de amostras. O banco de dados de amostras
        /// é constituído pelo diretório "amostras" e seus subdiretórios no diretório
        /// raiz do sistema (amostras\a, amostras\e, amostras\i, amostras\o, amostras\u).
        /// </summary>
        /// <param name="bitmap">desenho de vogal obtido da tela do programa.</param>
        /// <param name="vogal">vogal do alfabeto a qual o desenho representa.</param>
        public void InserirAmostra(Bitmap bitmap, String vogal) {
            //Diretório será relativo à vogal selecionada.
            String diretorio = @Application.StartupPath + @"\amostras\" + vogal;
            if (!Directory.Exists(diretorio)) {
                Directory.CreateDirectory(diretorio);
            }
            //O objetivo deste while é detectar a improvável situção de
            //gerar um nome de arquivo que já existe, e que não deve ser
            //usado para não sobrescrever uma imagem no banco de dados.
            bool salvo = false;
            do {                    
                String nomeArquivo = ObterNomeArquivo(diretorio, ".bmp");
                if (!File.Exists(nomeArquivo)) {
                    //O formato do arquivo é o .bmp (bitmap do Windows) em
                    //razão de não ser desejável a compressão da imagem, com
                    //a eventual perda de informações.
                    bitmap.Save(nomeArquivo, ImageFormat.Bmp);
                    salvo = true;
                }
            } while (!salvo);
        }

        /// <summary>
        /// Fazer a leitura pixel a pixel de um bitmap. Aonde for encontrado um pixel branco,
        /// no vetor de entrada gerado a partir desta imagem será gravado o valor 0.0,
        /// caso contrário, será gravado o valor 1.0. Pixels brancos são o fundo da imagem e 
        /// os pretos os contornos de uma vogal manuscrita.
        /// </summary>
        /// <param name="bitmap">desenho de vogal obtido da tela do programa.</param>
        /// <returns>vetor de double com valores 0's e 1's obtidos da leitura da imagem.</returns>
        private double[] ObterEntradas(Bitmap bitmap) {
            double[] entradas = new double[bitmap.Width * bitmap.Height];
            int idx = 0;
            for (int i = 0; i < bitmap.Width; i++) {
                for (int j = 0; j < bitmap.Height; j++) {
                    int rgb = bitmap.GetPixel(i, j).ToArgb();
                    if (rgb != Color.White.ToArgb()) {
                        entradas[idx] = 0.0d;
                    } else {
                        entradas[idx] = 1.0d;
                    }
                    ++idx;                                
                }
            }
            return entradas;
        }

        /// <summary>
        /// Obter a lista com todos os arquivos contidos em um diretório e também
        /// em seus subdiretórios.
        /// </summary>
        /// <param name="diretorio">diretório a serem obtidos os arquivos.</param>
        /// <returns>lista de arquivos contidos no diretório.</returns>
        private List<String> ObterListaArquivos(String diretorio) {
            List<String> listaArquivos = new List<String>();
            DirectoryInfo dirInfo = new DirectoryInfo(diretorio);
            foreach (FileInfo fileInfo in dirInfo.GetFiles()) {
                //Arquivos no diretório.
                listaArquivos.Add(fileInfo.FullName);
            }
            foreach (DirectoryInfo subDiretorio in dirInfo.GetDirectories()) {
                //Subdiretórios. Busca recursiva.
                List<String> lista = ObterListaArquivos(subDiretorio.FullName);
                listaArquivos.AddRange(lista);
            }
            return listaArquivos;
        }

        /// <summary>
        /// Classificar uma imagem de entrada como sendo uma vogal do alfabeto português.
        /// </summary>
        /// <param name="bitmap">desenho de vogal obtido da tela do programa.</param>
        /// <returns>Vogal reconhecida: -1 = ?, 0 = "a", 1 = "e", 2 = "i", 3 = "o", 4 = "u"</returns>
        public int ClassificarImagem(Bitmap bitmap) {
            int indice = -1;
            if (treinada) {
                //Obtém as entradas para a rede neural artificial a partir do bitmap.
                double[] entradas = ObterEntradas(bitmap);
                //Calcula as saídas para as entradas obtidas (requerido que a rede neural
                //esteja treinada).
                double[] saidasCalculadas = network.Compute(entradas);
                //Obtém o índice da vogal reconhecida: 0 = "a", 1 = "e", 2 = "i", 3 = "o",
                //4 = "u". O índice é obtido calculando-se qual neurônio está com a saída
                //ativada. Eventualmente, todos as saídas podem estar desativadas, daí o
                //índice será -1, indicando indefinido.
                for (int i = 0; i < saidasCalculadas.Length; i++) {
                    if (saidasCalculadas[i] == 1) {
                        indice = i;
                        break;
                    }
                }
                return indice;
            } else {
                throw new Exception(
                    "A rede neural ainda não foi treinada"
                );
            }          
        }

        /// <summary>
        /// Fazer o treinamento da rede neural com as amostras obtidas do banco de dados
        /// de amostras.
        /// </summary>
        public void Treinar() {
            //Cria a estrutura da rede neural artificial. Usamos a função limiar para a
            //ativação das saídas em 0 ou 1.
            //O valor 12100 é o número de entradas da rede neural que corresponde ao número
            //de pixels (110 * 110) de uma imagem obtida da tela do programa. O valor 5 
            //correponte aos 5 neurônios do perceptron. Abaixo definiremos as saídas-alvo 
            //mostrando como esses neurônios deverão ser ativados/desativados de acordo com 
            //a vogal representada.
            network = new ActivationNetwork(new ThresholdFunction(), 12100, 5);
            PerceptronLearning learning = new PerceptronLearning(network);
            String[] vogais = new String[]{"a", "e", "i", "o", "u"}; 
            int numeroAmostras = 0;
            foreach (String vogal in vogais) {
                String diretorio = @Application.StartupPath + @"\amostras\" + vogal;
                List<String> arquivos = ObterListaArquivos(diretorio);
                numeroAmostras += arquivos.Count;
            }
            int indice = 0;
            double[][] amostras = new double[numeroAmostras][];
            double[][] saidasAlvo = new double[numeroAmostras][];
            foreach (String vogal in vogais) {                    
                String diretorio = @Application.StartupPath + @"\amostras\" + vogal;
                List<String> arquivos = ObterListaArquivos(diretorio);
                foreach (String arquivo in arquivos) {
                    //Obtém a imagem gravada no arquivo do banco de dados de amostras
                    //com 110 x 110 pixels.
                    Bitmap bitmap = new Bitmap(arquivo, false);
                    //Obtém as entradas para a rede neural artificial a partir da imagem
                    //recuperada do banco de dados de amostras (binarização da imagem).
                    amostras[indice] = ObterEntradas(bitmap); 
                    //As saídas-alvo para cada vogal são as seguintes:
                    //
                    //   Vogal       Saídas Alvo
                    //   a           1-0-0-0-0
                    //   e           0-1-0-0-0
                    //   i           0-0-1-0-0
                    //   o           0-0-0-1-0
                    //   u           0-0-0-0-1
                    //
                    //Isso representa um esquema de ativação/desativação de neurônios de
                    //acordo com a amostra apresentada representando uma vogal específica.
                    //Na forma como estão definidas as saídas-alvo, o neurônio da posição 1
                    //indica a vogal "a", logo, seta-se a sua ativação (sinal em 1) e a
                    //desativação dos demais neurônios quando uma amostra de "a" for apresentada,
                    //da mesma forma, o neurônio da posição 2 indica a vogal "e" e seta-se
                    //a sua ativação quando uma amostra de "e" for apresentada, desativando-se
                    //todos os demais neurônios, e assim sucessivamente.
                    switch (vogal) {
                        case "a": saidasAlvo[indice] = new double[]{1.0d, 0.0d, 0.0d, 0.0d, 0.0d}; break;
                        case "e": saidasAlvo[indice] = new double[]{0.0d, 1.0d, 0.0d, 0.0d, 0.0d}; break;
                        case "i": saidasAlvo[indice] = new double[]{0.0d, 0.0d, 1.0d, 0.0d, 0.0d}; break;
                        case "o": saidasAlvo[indice] = new double[]{0.0d, 0.0d, 0.0d, 1.0d, 0.0d}; break;
                        case "u": saidasAlvo[indice] = new double[]{0.0d, 0.0d, 0.0d, 0.0d, 1.0d}; break;
                    }
                    indice++;
                }
            }
            //Neste ponto ocorre o treinamento da rede neural, com o ajustamento dos
            //pesos sinápticos. Especificamente neste ponto o computador aprende a
            //classificar um padrão complexo obtido do ambiente que neste programa é 
            //em forma de imagem, mas poderia ser dados dos mais diversos tipos.
            //No jorgão técnico, este tipo de aprendizado é denominado de aprendizado
            //supervisionado, pois temos que mostrar para a rede neural um exemplo de
            //padrão de entrada e as saída-alvo específica que queremos que ela se 
            //ajuste. Veja que no processo acima, para cada imagem de entrada nós
            //tivemos que associar uma saída-alvo apropriada para o padrão que ela
            //representa, no caso, uma vogal do alfabeto português.
            double erro = 0.0d;
            do {
                erro = learning.RunEpoch(amostras, saidasAlvo);
            } while (erro > 0.01);
            //Treinada a rede neural artificial, vamos salvar este aprendizado em um 
            //arquivo em disco. Se isso não for feito, teremos que treinar a rede toda
            //vez que formos utilizar o programa.
            String arquivoAprendizado = @Application.StartupPath + @"\aprendizado.dat";
            if (File.Exists(arquivoAprendizado)) {
                File.Delete(arquivoAprendizado);
            }
            network.Save(arquivoAprendizado);
            treinada = true;
        }

        /// <summary>
        /// Indica se o perceptron está treinado (true) ou não (false).
        /// </summary>
        public bool Treinado {
            get {return treinada;}
        }

    }

}
