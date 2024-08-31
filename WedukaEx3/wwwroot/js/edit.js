document.addEventListener('DOMContentLoaded', () => {
    let contatoIndex = document.querySelectorAll('#contatos-table tbody tr').length;
    const pessoaId = document.querySelector('input[name="Id"]').value; // Obtendo o ID da pessoa do campo oculto

    function formatPhone(value) {
        if (value.length <= 2) {
            return value.replace(/(\d{0,2})/, '($1');
        } else if (value.length <= 7) {
            return value.replace(/(\d{2})(\d{0,5})/, '($1) $2');
        } else if (value.length <= 10) {
            return value.replace(/(\d{2})(\d{4})(\d{0,4})/, '($1) $2-$3');
        } else {
            return value.replace(/(\d{2})(\d{5})(\d{0,4})/, '($1) $2-$3');
        }
    }
    function applyMasks() {
        document.querySelectorAll('#contatos-table tbody tr').forEach(row => {
            const tipo = row.querySelector('select').value;
            const input = row.querySelector('input[name$=".Valor"]');

            // Remover event listeners antigos
            input.removeEventListener('input', handleInput);

            switch (tipo) {
                case 'Telefone':
                case 'WhatsApp':
                    input.addEventListener('input', handleInput);
                    break;
                case 'Email':
                    input.addEventListener('input', function () {
                        let value = this.value;
                        if (value.length > 255) {
                            this.value = value.slice(0, 255);
                        }
                    });
                    break;
            }
        });
    }

    function handleInput(event) {
        let value = event.target.value.replace(/\D/g, '');
        if (value.length > 11) {
            value = value.slice(0, 11);
        }
        event.target.value = formatPhone(value);
    }

    // Adicionar novo contato
    document.getElementById('add-contact').addEventListener('click', () => {
        const tableBody = document.querySelector('#contatos-table tbody');

        // Adicionar uma nova linha à tabela
        const row = document.createElement('tr');
        row.innerHTML = `
                                    <td>
                                        <select name="Contatos[${contatoIndex}].Tipo" class="form-select" required>
                                            <option value="">Selecione</option>
                                            <option value="Telefone">Telefone</option>
                                            <option value="Email">Email</option>
                                            <option value="WhatsApp">WhatsApp</option>
                                        </select>
                                    </td>
                                    <td>
                                        <input type="text" name="Contatos[${contatoIndex}].Valor" class="form-control" required>
                                        <input type="hidden" name="Contatos[${contatoIndex}].Id" value="0" />
                                        <input type="hidden" name="Contatos[${contatoIndex}].PessoaId" value="${pessoaId}" />
                                    </td>
                                    <td>
                                        <button type="button" class="btn btn-danger remove-contact">Remover</button>
                                    </td>
                                `;

        tableBody.appendChild(row);

        contatoIndex++;
    });

    // Remover contato
    document.querySelector('#contatos-table').addEventListener('click', (event) => {
        if (event.target.classList.contains('remove-contact')) {
            event.target.closest('tr').remove();
        }
    });

    // Aplicar máscaras quando a tabela é atualizada
    document.querySelector('#contatos-table').addEventListener('change', (event) => {
        if (event.target.tagName === 'SELECT') {
            applyMasks();
        }
    });

    // Aplicar máscaras ao carregar a página
    applyMasks();
});