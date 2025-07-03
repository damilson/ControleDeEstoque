import React, { type ChangeEvent, type FormEvent, useEffect, useState } from 'react';
import api from '../../axios';

type EnderecoApi = {
    logradouro?: string | null;
    numero?: string | null;
    complemento?: string | null;
    bairro?: string | null;
    cidade?: string | null;
    estado?: string | null;
    cep?: string | null;
};

type UsuarioApi = {
    id?: string; 
    nomeCompleto?: string | null;
    cpf?: string | null;
    rg?: string | null;
    dataNascimento?: string | null; 
    telefone?: string | null;
    celular?: string | null;
    email?: string | null;
    sexo?: string | null;
    senha?: string | null;
    senhaHash?: string | null;
    perfilId: number;
    endereco: EnderecoApi;
};

const UsuariosPage: React.FC = () => {
    const [usuarios, setUsuarios] = useState<UsuarioApi[]>([]);
    const [form, setForm] = useState<UsuarioApi>({
        nomeCompleto: null,
        cpf: null,
        rg: null,
        dataNascimento: null,
        telefone: null,
        celular: null,
        email: null,
        sexo: null,
        senha: null,
        senhaHash: null,
        perfilId: 0,
        endereco: {
            logradouro: null,
            numero: null,
            complemento: null,
            bairro: null,
            cidade: null,
            estado: null,
            cep: null
        }
    });

    const fetchUsuarios = async () => {
        try {
            const response = await api.get<UsuarioApi[]>('/api/usuario/login/dados');
            setUsuarios(response.data);
        } catch (error) {
            console.error('Erro ao buscar usuários', error);
        }
    };

    const handleChange = (e: ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        const { name, value } = e.target;

        if (name in form.endereco) {
            setForm(prev => ({
                ...prev,
                endereco: {
                    ...prev.endereco,
                    [name]: value || null
                }
            }));
        } else {
            setForm(prev => ({
                ...prev,
                [name]: name === 'perfilId' ? parseInt(value) : (value || null),
            }));
        }
    };

    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();
        try {
            if (form.id) {
                await api.put('/api/usuario', form);
            } else {
                await api.post('/api/usuario', form);
            }
            resetForm();
            fetchUsuarios();
        } catch (error) {
            console.error('Erro ao salvar usuário', error);
        }
    };

    const resetForm = () => {
        setForm({
            nomeCompleto: null,
            cpf: null,
            rg: null,
            dataNascimento: null,
            telefone: null,
            celular: null,
            email: null,
            sexo: null,
            senha: null,
            senhaHash: null,
            perfilId: 0,
            endereco: {
                logradouro: null,
                numero: null,
                complemento: null,
                bairro: null,
                cidade: null,
                estado: null,
                cep: null
            }
        });
    };

    const deleteUsuario = async (id?: string) => {
        if (!id) return;
        try {
            await api.delete(`/api/usuario?id=${id}`);
            fetchUsuarios();
        } catch (error) {
            console.error('Erro ao excluir usuário', error);
        }
    };

    const editUsuario = (usuario: UsuarioApi) => {
        setForm({
            ...usuario,
            
            endereco: usuario.endereco || {
                logradouro: null,
                numero: null,
                complemento: null,
                bairro: null,
                cidade: null,
                estado: null,
                cep: null
            }
        });
    };

    useEffect(() => {
        fetchUsuarios();
    }, []);

    return (
        <div className="p-6 max-w-6xl mx-auto">
            <h1 className="text-2xl font-bold mb-4">Cadastro de Usuarios</h1>
            <form onSubmit={handleSubmit} className="space-y-4 border p-4 rounded shadow">
                <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                    
                    <input
                        type="text"
                        name="nomeCompleto"
                        placeholder="Nome Completo"
                        value={form.nomeCompleto || ''}
                        onChange={handleChange}
                        className="w-full border p-2 rounded"
                    />
                    <input
                        type="text"
                        name="cpf"
                        placeholder="CPF"
                        value={form.cpf || ''}
                        onChange={handleChange}
                        className="w-full border p-2 rounded"
                    />
                    <input
                        type="text"
                        name="rg"
                        placeholder="RG"
                        value={form.rg || ''}
                        onChange={handleChange}
                        className="w-full border p-2 rounded"
                    />
                    <input
                        type="date"
                        name="dataNascimento"
                        placeholder="Data de Nascimento"
                        value={form.dataNascimento ? form.dataNascimento.split('T')[0] : ''}
                        onChange={handleChange}
                        className="w-full border p-2 rounded"
                    />
                    <input
                        type="tel"
                        name="telefone"
                        placeholder="Telefone"
                        value={form.telefone || ''}
                        onChange={handleChange}
                        className="w-full border p-2 rounded"
                    />
                    <input
                        type="tel"
                        name="celular"
                        placeholder="Celular"
                        value={form.celular || ''}
                        onChange={handleChange}
                        className="w-full border p-2 rounded"
                    />
                    <input
                        type="email"
                        name="email"
                        placeholder="Email"
                        value={form.email || ''}
                        onChange={handleChange}
                        className="w-full border p-2 rounded"
                    />
                    <select
                        name="sexo"
                        value={form.sexo || ''}
                        onChange={handleChange}
                        className="w-full border p-2 rounded"
                    >
                        <option value="">Selecione o sexo</option>
                        <option value="M">Masculino</option>
                        <option value="F">Feminino</option>
                        <option value="O">Outro</option>
                    </select>
                    <input
                        type="password"
                        name="senha"
                        placeholder="Senha"
                        value={form.senha || ''}
                        onChange={handleChange}
                        className="w-full border p-2 rounded"
                    />
                    <select
                        name="perfilId"
                        value={form.perfilId}
                        onChange={handleChange}
                        className="w-full border p-2 rounded"
                    >
                        <option value={0}>Usuário Comum</option>
                        <option value={1}>Administrador</option>
                        <option value={2}>Gerente</option>
                    </select>
                </div>

                <h3 className="text-lg font-semibold mt-6">Endereço</h3>
                <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                    <input
                        type="text"
                        name="logradouro"
                        placeholder="Logradouro"
                        value={form.endereco?.logradouro || ''}
                        onChange={handleChange}
                        className="w-full border p-2 rounded"
                    />
                    <input
                        type="text"
                        name="numero"
                        placeholder="Número"
                        value={form.endereco?.numero || ''}
                        onChange={handleChange}
                        className="w-full border p-2 rounded"
                    />
                    <input
                        type="text"
                        name="complemento"
                        placeholder="Complemento"
                        value={form.endereco?.complemento || ''}
                        onChange={handleChange}
                        className="w-full border p-2 rounded"
                    />
                    <input
                        type="text"
                        name="bairro"
                        placeholder="Bairro"
                        value={form.endereco?.bairro || ''}
                        onChange={handleChange}
                        className="w-full border p-2 rounded"
                    />
                    <input
                        type="text"
                        name="cidade"
                        placeholder="Cidade"
                        value={form.endereco?.cidade || ''}
                        onChange={handleChange}
                        className="w-full border p-2 rounded"
                    />
                    <select
                        name="estado"
                        value={form.endereco?.estado || ''}
                        onChange={handleChange}
                        className="w-full border p-2 rounded"
                    >
                        <option value="">Selecione o estado</option>
                        <option value="AC">Acre</option>
                        <option value="AL">Alagoas</option>
                        <option value="AP">Amapa</option>
                        <option value="AM">Amazonas</option>
                        <option value="BA">Bahia</option>
                        <option value="CE">Ceara</option>
                        <option value="DF">Distrito Federal</option>
                        <option value="ES">Espirito Santo</option>
                        <option value="GO">Goiás</option>
                        <option value="MA">Maranhao</option>
                        <option value="MT">Mato Grosso</option>
                        <option value="MS">Mato Grosso do Sul</option>
                        <option value="MG">Minas Gerais</option>
                        <option value="PA">Para</option>
                        <option value="PB">Paraiba</option>
                        <option value="PR">Parana</option>
                        <option value="PE">Pernambuco</option>
                        <option value="PI">Piaui</option>
                        <option value="RJ">Rio de Janeiro</option>
                        <option value="RN">Rio Grande do Norte</option>
                        <option value="RS">Rio Grande do Sul</option>
                        <option value="RO">Rondonia</option>
                        <option value="RR">Roraima</option>
                        <option value="SC">Santa Catarina</option>
                        <option value="SP">Sao Paulo</option>
                        <option value="SE">Sergipe</option>
                        <option value="TO">Tocantins</option>
                        
                    </select>
                    <input
                        type="text"
                        name="cep"
                        placeholder="CEP"
                        value={form.endereco?.cep || ''}
                        onChange={handleChange}
                        className="w-full border p-2 rounded"
                    />
                </div>

                <div className="flex justify-end space-x-4 mt-6">
                    <button
                        type="button"
                        onClick={resetForm}
                        className="bg-gray-500 text-white px-4 py-2 rounded"
                    >
                        Limpar
                    </button>
                    <button
                        type="submit"
                        className="bg-blue-600 text-white px-4 py-2 rounded"
                    >
                        {form.id ? 'Atualizar' : 'Cadastrar'} Usuário
                    </button>
                </div>
            </form>

            <h2 className="text-xl font-semibold mt-8 mb-4">Lista de Usuários</h2>
            <div className="overflow-x-auto">
                <table className="w-full border">
                    <thead className="bg-gray-200">
                        <tr>
                            <th className="p-3 border text-left">Nome</th>
                            <th className="p-3 border text-left">CPF</th>
                            <th className="p-3 border text-left">Email</th>
                            <th className="p-3 border text-left">Perfil</th>
                            <th className="p-3 border text-left">Ações</th>
                        </tr>
                    </thead>
                    <tbody>
                        {usuarios.map((usuario) => (
                            <tr key={usuario.id} className="hover:bg-gray-50">
                                <td className="p-3 border">{usuario.nomeCompleto || '-'}</td>
                                <td className="p-3 border">{usuario.cpf || '-'}</td>
                                <td className="p-3 border">{usuario.email || '-'}</td>
                                <td className="p-3 border">
                                    {usuario.perfilId === 0 ? 'Usuário' :
                                        usuario.perfilId === 1 ? 'Admin' : 'Gerente'}
                                </td>
                                <td className="p-3 border space-x-2">
                                    <button
                                        onClick={() => editUsuario(usuario)}
                                        className="text-blue-600 hover:text-blue-800"
                                    >
                                        Editar
                                    </button>
                                    <button
                                        onClick={() => deleteUsuario(usuario.id)}
                                        className="text-red-600 hover:text-red-800"
                                    >
                                        Excluir
                                    </button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
};

export default UsuariosPage;