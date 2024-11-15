import { useEffect, useState } from 'react';
import axios from 'axios';
import PropTypes from 'prop-types';

const LabPage = ({ labNumber }) => {
    const [labData, setLabData] = useState(null);
    const [inputContent, setInputContent] = useState('');
    const [outputContent, setOutputContent] = useState('');
    const labNumberInt = parseInt(labNumber, 10);

    // Функція для завантаження даних лабораторної роботи з сервера
    useEffect(() => {
        const fetchLabData = async () => {
            try {
                const response = await axios.get(`/api/labs/lab${labNumber}`);
                setLabData(response.data);
            } catch (error) {
                console.error('Error fetching lab data:', error);
            }
        };

        setInputContent('');
        setOutputContent('');
        fetchLabData();
    }, [labNumber]);

    // Функція для завантаження файлу та його вмісту
    const handleFileChange = (e) => {
        const file = e.target.files[0];
        const reader = new FileReader();
        reader.onload = (event) => {
            setInputContent(event.target.result);
        };
        reader.readAsText(file);
    };

    // Функція для відправки даних на сервер
    const handleFormSubmit = async (e) => {
        e.preventDefault();
        const formData = new FormData();

        const inputFile = e.target.elements.inputFile.files[0];
        formData.append('inputFile', inputFile);
        formData.append('labNumber', labNumberInt); 

        try {
            const response = await axios.post('/api/labs/process', formData, {
                headers: {
                    'Content-Type': 'multipart/form-data',
                },
            });
            if (response.data.Output) {
                setOutputContent(response.data.Output);
            } else if (response.data.output) {
                setOutputContent(response.data.output);
            } else {
                throw new Error('Unexpected response format');
            }
        } catch (error) {
            console.error('Error during form submission:', error);
            alert('Error: ' + (error.response?.data?.Error || error.message));
        }
    };

    if (!labData) {
        return <p>Loading...</p>;
    }

    return (
        <div className="container mt-4">
            <div className="card shadow-sm">
                <div className="card-header">
                    <h2>Лабораторна робота № {labData.labNumber}</h2>
                    <h4>Варіант {labData.variant}</h4>
                </div>

                <div className="card-body">
                    {/* Завдання */}
                    <section>
                        <h5>Завдання:</h5>
                        <p>{labData.description}</p>
                    </section>

                    {/* Вхідні та вихідні дані */}
                    <section>
                        <h5>Вхідні дані:</h5>
                        <p>{labData.inputDescription}</p>

                        <h5>Вихідні дані:</h5>
                        <p>{labData.outputDescription}</p>
                    </section>

                    {/* Приклади */}
                    <section>
                        <h5>Приклади:</h5>
                        <table className="table table-striped">
                            <thead>
                                <tr>
                                    <th>INPUT.TXT</th>
                                    <th>OUTPUT.TXT</th>
                                </tr>
                            </thead>
                            <tbody>
                                {labData.testCases && labData.testCases.length > 0 ? (
                                    labData.testCases.map((test, index) => (
                                        <tr key={index}>
                                            <td><pre>{test.input}</pre></td>
                                            <td><pre>{test.output}</pre></td>
                                        </tr>
                                    ))
                                ) : (
                                    <tr>
                                        <td colSpan="2">No test cases available</td>
                                    </tr>
                                )}
                            </tbody>
                        </table>
                    </section>

                    {/* Форма завантаження */}
                    <section className="mt-4">
                        <h5>Перевірка розв&aposязку:</h5>
                        <form onSubmit={handleFormSubmit} className="form-inline">
                            <input type="hidden" name="labNumber" value={labNumberInt} />

                            <div className="form-group mb-3">
                                <label htmlFor="inputFile" className="form-label mr-2">Вхідний файл:</label>
                                <input
                                    type="file"
                                    className="form-control-file"
                                    id="inputFile"
                                    name="inputFile"
                                    required
                                    onChange={handleFileChange}
                                />
                            </div>

                            <div className="form-group mb-3">
                                <label htmlFor="inputContent" className="form-label">Вміст вхідного файлу:</label>
                                <textarea
                                    className="form-control"
                                    id="inputContent"
                                    rows="4"
                                    value={inputContent}
                                    readOnly
                                />
                            </div>

                            <div className="form-group mb-3">
                                <label htmlFor="outputContent" className="form-label">Результат:</label>
                                <textarea
                                    className="form-control"
                                    id="outputContent"
                                    rows="4"
                                    value={outputContent}
                                    readOnly
                                />
                            </div>

                            <button type="submit" className="btn btn-outline-primary">Перевірити</button>
                        </form>
                    </section>
                </div>
            </div>
        </div>
    );
};

LabPage.propTypes = {
    labNumber: PropTypes.string.isRequired,
};

export default LabPage;
