import { Component } from "react";
import NavBar from "../Components/NavBar";
import React from "react";
import PlanilhaService from "../Services/PlanilhaService";
import Swal from 'sweetalert2';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTable } from '@fortawesome/free-solid-svg-icons';
import planilhaEx from "../Assets/planilhaEx.xlsx"
import "../Styles/HomePageStyle.css"
class HomePage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      planilha: null,
      hasError: false,
      error: null,
    };
  }

  componentDidCatch(error, errorInfo) {
    this.setState({
      hasError: true,
      error,
    });
  }
    handleFileChange = (event) => {
        const planilha = event.target.files[0];
        if (planilha) {
          // Atualize o estado para incluir a informação sobre a planilha
          this.setState({ planilha });
        }
      };

      handleClick = async () => {
        const { planilha } = this.state;
        // Exibe o SweetAlert enquanto aguarda a conclusão do método
        Swal.fire({
          title: 'Processando...',
          showLoading: true,
          allowOutsideClick: false,
          onBeforeOpen: () => {
            Swal.showLoading();
          },
        });
    
        // try {
          // Chama o método na classe de serviço
          const resultado = PlanilhaService.SalvarPlanilha(planilha).then((result) => {
            Swal.fire({
              icon: 'success',
              title: 'Concluído!',
              text: 'A planilha foi processada com sucesso.',
            });
          }).catch((e) =>
          {
            e.catch(error => 
            {
              Swal.fire({
                icon: 'error',
                title: 'Erro!',
                text: error.message,
              });
            });

          });
          // Fecha o SweetAlert após a conclusão bem-sucedida

        // } catch (error) {
        //   // Trata erros, se houver

        // }
      };
    
    render()
    {
        const { planilha } = this.state;
        return (
            <>
                <NavBar  Tela="home"/>

                <div className=" row flex-grow-1 divPrincipal">
                    <div className="row">
                        <h1>
                            <FontAwesomeIcon icon={faTable} className="iconeHome" />
                            Sistema de importação de planilhas
                        </h1>
                    </div>
                    <div className="row">
                        <h5 className="onioSaSmall">Onion S.A</h5>
                    </div>
                    <div>
                        <p className="onioSaSmall">O sistema de importação de planilhas da <b>Onion S.A</b> é o sistema que irá receber a sua planilha de pedidos e lhe retornar dashboards informativos sobre os seus pedidos.</p>
                    </div>
                    <hr className="linhaHorizontal" />
                    <div className="row texto" >
                        <h3 className="onionSaSmall">Vamos iniciar!</h3>
                        <ol >
                            <li className="onionSaSmall">Primeiro você precisará que sua planilha possua os seguintes campos exatamente igual a essa: <a href={planilhaEx} download="planilha-exemplo" target="_blank">Planilha de exemplo</a>;</li>
                            <li className="onionSaSmall">Em seguida, você irá clicar em <b>"Escolher arquivo"</b> para selecionar sua planilha.</li>
                        </ol>
                    </div>
                    <div className="row texto">
                        <div class="mb-3">
                            <div className="col-6">
                                <label for="inputPlanilha" className="form-label">Insira aqui a sua planilha</label>
                                <input className="form-control" type="file" id="inputPlanilha" accept=".xls, .xlsx, .csv" onChange={this.handleFileChange}/>
                            </div>

                        </div>
                    </div>
                    {planilha ?
                        <div className="row texto">
                            <p >A planilha <b>{planilha ? planilha.name : ''}</b> foi selecionada!</p>
                            <div className="col-6">
                                <button className="btn btn-dark" onClick={this.handleClick}>Enviar planilha</button>

                            </div>
                        </div>
                    : null}
                </div>

            </>
        );
    }
}

// const HomePage = props => (
//     <>
//         <div className="row">
//             <h1>Bem vindo á Onion S.A</h1>
//         </div>
//     </>
// );

export default HomePage;