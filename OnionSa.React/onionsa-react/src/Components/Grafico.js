import React, { useEffect, useRef, useState } from 'react';
import Chart from 'chart.js/auto';

const Grafico = (props) => {
  const chartRef = useRef(null);
  const [chart, setChart] = useState(null);

  useEffect(() => {
    const ctx = chartRef.current.getContext('2d');

    if (chart) {
      // Se um gráfico existir, destrua-o antes de criar um novo
      chart.destroy();
    }

    const chartInstance = new Chart(ctx, {
      type: 'pie',
      data: {
        labels: Object.keys(props.Data),
        datasets: [
          {
            data: Object.values(props.Data),
            backgroundColor: [
              'rgba(255, 99, 132, 0.8)',
              'rgba(54, 162, 235, 0.8)',
              'rgba(255, 206, 86, 0.8)',
              'rgba(75, 192, 192, 0.8)',
              'rgba(153, 102, 255, 0.8)',
            ],
          },
        ],
      },
      options: {
        plugins: {
          datalabels: {
            color: '#fff',
            font: {
              weight: 'bold',
            },
          },
        },
      },
    });

    setChart(chartInstance);

    return () => {
      // Certifique-se de destruir o gráfico ao desmontar o componente
      if (chartInstance) {
        chartInstance.destroy();
      }
    };
  }, [props.Data]);

  return <canvas ref={chartRef} />;
};

export default Grafico;
