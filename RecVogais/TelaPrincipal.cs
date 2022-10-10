using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using RecVogais.Properties;
using System.IO;
using System.Drawing.Imaging;
using AForge;
using AForge.Neuro;
using AForge.Neuro.Learning;

namespace RecVogais {
    
    public partial class TelaPrincipal : Form {

        //Perceptron simples para o reconhecimento de vogais manuscritas.
        private Perceptron perceptron;
        //Instância de Graphics para desenho no PictureBox.
        public Graphics graphics;
        //Instância de Point para desenho no PictureBox.
        private System.Drawing.Point point = System.Drawing.Point.Empty;
        //Indica que o usuário clicou e está segurando o botão do mouse
        //na área do componente PictureBox, aonde deverá desenhar a vogal.
        private bool mouseDown = false;
        //Evento para classificação de uma imagem de entrada.
        private EventHandler clickEvent1;
        //Evento para inserção de uma amostra no banco de dados.
        private EventHandler clickEvent2;        
        
        public TelaPrincipal() {
            InitializeComponent();
            perceptron = new Perceptron();            
            pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
            graphics = Graphics.FromImage(pictureBox.Image);
            pictureBox.Cursor = new Cursor(Resources.pen.GetHicon());
            clickEvent1 = new System.EventHandler(ClassificarImagemClick);
            clickEvent2 = new System.EventHandler(InserirAmostraClick);
            Control.CheckForIllegalCrossThreadCalls = false;
            ConfigurarControles();
        }

        #region Perceptron Simples (Rede Neural Artificial)

        //Salvar o desenho na tela como uma amostra no banco de dados de amostras.
        private void InserirAmostra() {
            try {
                String vogal = "";
                int idx = listaVogais.SelectedIndex;
                switch (idx) {
                    case 0: vogal = "a"; break;
                    case 1: vogal = "e"; break;
                    case 2: vogal = "i"; break;
                    case 3: vogal = "o"; break;
                    case 4: vogal = "u"; break;
                }
                Bitmap bitmap = ObterVogalDesenhada();
                if (bitmap != null) {
                    perceptron.InserirAmostra(bitmap, vogal);
                    LimparImagem();
                } else {
                    throw new Exception(
                        "Precisa desenhar a vogal para inserir " +
                        "como amostra no banco de dados."
                    );
                }               
            } catch (Exception ex) {
                MessageBox.Show(
                    this,
                    ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        //Classificar uma imagem desenhada no componente PictureBox como uma vogal,
        //que será selecionada na ListBox à direita da tela.
        //Por exemplo, suponha que você desenhe a vogal "a" na PictureBox. Feito isso,
        //você vai clicar no botão "Classificar", e é esperado que o "a" na ListBox
        //seja selecionado.
        //Cabe aqui um apontamento importante. Sistemas baseados em inteligência
        //artificial não funcionam da mesma forma determinística que os sistemas
        //convencionais. Então, ele pode selecionar uma vogal que não seja um "a",
        //mesmo você tendo desenhado corretamente. A qualidade dos resultados
        //depende de uma série de fatores, entre eles, o processamento de imagem
        //para a extração de características que facilitem o reconhecimento, a 
        //escolha da arquitetura correta de rede neural para o problema a ser
        //abordado, a escolha de amostras representativas, testes de eficácia
        //da solução, entre outras coisas.
        private void ClassificarImagem() {
            try {
                botaoClassificar.Enabled = false;
                Cursor = Cursors.WaitCursor;
                labelVogalIrreconhecivel.Visible = false;
                if (perceptron.Treinado) {
                    Bitmap bitmap = ObterVogalDesenhada();
                    if (bitmap != null) {
                        //O índice da vogal retornado pelo método ClassificarImagem é
                        //convenientemente ajustado para se adequar aos índices relativos
                        //da lista.
                        int indice = perceptron.ClassificarImagem(bitmap);
                        listaVogais.SelectedIndex = indice;
                        if (listaVogais.SelectedIndex == -1) {
                            labelVogalIrreconhecivel.Visible = true;
                        }
                    } else {
                        throw new Exception(
                            "Desenhe uma vogal para ser identificada."
                        );
                    }
                } else {
                    throw new Exception(
                        "A rede neural ainda não foi treinada"
                    );
                }
            } catch (Exception ex) {
                MessageBox.Show(
                    this,
                    ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            } finally {
                botaoClassificar.Enabled = true;
                Cursor = Cursors.Default;
            }            
        }

        //Treinar a rede neural com cada imagem obtida do banco de dados de
        //amostras.
        private void TreinarRedeNeural() {
            try {
                botaoClassificar.Enabled = false;
                botaoTreinar.Enabled = false;
                botaoLimpar.Enabled = false;
                opcaoClassificacao.Enabled = false;
                opcaoTreinamento.Enabled = false;
                pictureBox.Enabled = false;
                barraNotificacao.Items[0].Text = "Treinando a Rede Neural";
                Cursor = Cursors.WaitCursor;
                listaVogais.Enabled = false;
                this.Refresh();
                perceptron.Treinar();
            } catch (Exception ex) {
                MessageBox.Show(
                    this,
                    ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            } finally {
                botaoClassificar.Enabled = true;
                botaoTreinar.Enabled = true;
                botaoLimpar.Enabled = true;
                opcaoClassificacao.Enabled = true;
                opcaoTreinamento.Enabled = true;
                pictureBox.Enabled = true;
                barraNotificacao.Items[0].Text = "Treinamento";
                Cursor = Cursors.Default;
                listaVogais.Enabled = true;
            }
        }

        #endregion

        #region Controles de interface gráfica do usuário

        //Configurar os controles da interface do usuário para refletir o modo
        //de operação selecionado. Se o RadioButton "Classificação" for o
        //selecionado, vai exibir um botão "Classificar", que após clicado
        //irá obter a imagem desenhada no PictureBox e classificá-la de acordo
        //com o aprendizado da rede neural. Se o RadioButton selecionado for o
        //"Treinamento", vai exibir os botões "Inserir" e "Treinar", além de
        //habilitar a seleção das vogais da lista direita. O botão "Inserir"
        //serve para a adição de amostras no banco de dados. O botão "Treinar"
        //serve para treinar a rede neural, ou seja, ajustar os pesos livres da
        //rede de modo a qualificá-la para classificar padrões.
        private void ConfigurarControles() {
            //Remove todos os tratadores de evento de click do mouse do botão
            //"botaoClassificar". Esse mesmo componente tem duplo papel. No 
            //modo "Classificação" ele servirá para "reconhecer" a vogal desenhada
            //no PictureBox. No modo "Treinamento" ele assume o papel de inserir
            //amostras no banco de dados.
            //Esse duplo papel é exclusivamente pelo fato de não ter que criar
            //mais controles na tela, o que dificultaria um pouco para posicionar
            //no código ao alternar entre os modos. Mantém-se o componente na sua 
            //posição e troca o texto de exibição e o tratador de eventos, sem a
            //necessidade de criar um terceiro componente e posicioná-lo no mesmo
            //local de "botaoClassificar" para manter a interface.
            botaoClassificar.Click -= clickEvent1;
            botaoClassificar.Click -= clickEvent2;
            labelVogalIrreconhecivel.Visible = false;
            if (opcaoClassificacao.Checked) {
                listaVogais.Enabled = false;
                barraNotificacao.Items[0].Text = "Classificação";
                botaoClassificar.Text = "Classificar";
                //Tratador de evento de click do mouse para classificação
                //de padrões.
                botaoClassificar.Click += clickEvent1;                
                botaoTreinar.Visible = false;
                listaVogais.SelectedIndex = -1;
            } else {
                listaVogais.Enabled = true;                
                barraNotificacao.Items[0].Text = "Treinamento";
                botaoClassificar.Text = "Adicionar";
                //Tratador de evento de click do mouse para inserir amostra
                //para o banco de dados.
                botaoClassificar.Click += clickEvent2;
                botaoTreinar.Visible = true;
                listaVogais.SelectedIndex = 0;
            }
        }

        //Obter a vogal desenhada no Picturebox. Caso não tenha desenhado a vogal,
        //retorna null.
        private Bitmap ObterVogalDesenhada() {
            bool vazia = true;
            Bitmap bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            Rectangle rec = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            pictureBox.DrawToBitmap(bitmap, rec);
            for (int i = 0; i < bitmap.Width && vazia; i++) {
                for (int j = 0; j < bitmap.Height && vazia; j++) {
                    if (bitmap.GetPixel(i, j).ToArgb() != Color.White.ToArgb()) {
                        vazia = false;
                    }
                }
            }
            return !vazia ? bitmap : null;
        }

        //Limpar o desenho no PictureBox, pintando todos os pixels de branco.
        private void LimparImagem() {
            graphics.Clear(Color.White);
            pictureBox.Invalidate();
        }

        //Tratador de evento que aciona a classificação de uma imagem.
        private void ClassificarImagemClick(object sender, EventArgs e) {
            ClassificarImagem();
        }

        //Tratador de evento que aciona a inserção de amostras no banco de dados.
        private void InserirAmostraClick(object sender, EventArgs e) {
            InserirAmostra();
        }

        //Registrar que o botão do mouse foi pressionado.
        private void CaixaPinturaMouseDown(object sender, MouseEventArgs e) {
            point = e.Location; //Local do clique.
            mouseDown = true; //Indica o clique.
        }

        //Registrar o movimento do cursor.
        private void CaixaPinturaMouseMove(object sender, MouseEventArgs e) {
            if (mouseDown == true) {
                if (point != null) {
                    //Desenha um linha contínua ao longo do percurso do cursor.
                    Pen pen = new Pen(Color.Black, 5.0f);
                    pen.StartCap = LineCap.Round;
                    pen.EndCap = LineCap.Round;
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.DrawLine(pen, point, e.Location);
                    point = e.Location;
                    pen.Dispose();
                }
            }
            pictureBox.Invalidate();
        }

        //Registrar que o botão do mouse foi liberado.
        private void CaixaPinturaMouseUp(object sender, MouseEventArgs e) {
            point = e.Location;
            mouseDown = false;
        }

        private void BotaoLimparClick(object sender, EventArgs e) {
            LimparImagem();
        }

        private void RadioButtonCheckedChanged(object sender, EventArgs e) {
            ConfigurarControles();
        }

        private void BotaoTreinarClick(object sender, EventArgs e) {
             TreinarRedeNeural();
        }

        #endregion
        
    }

}
