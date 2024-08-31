$(document).ready(function () {
    let contatoIndex = 0;

    $('#add-contact').on('click', function () {
        const tableBody = $('#contatos-table tbody');
        const row = $(`
                                    <tr>
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
                                        </td>
                                        <td>
                                            <button type="button" class="btn btn-danger remove-contact">Remover</button>
                                        </td>
                                    </tr>
                                `);

        tableBody.append(row);
        applyMasks();

        contatoIndex++;
    });

    $('#contatos-table').on('click', '.remove-contact', function () {
        $(this).closest('tr').remove();
    });

    // Função para aplicar as máscaras
    function applyMasks() {
        $('#contatos-table tbody tr').each(function () {
            const tipo = $(this).find('select').val();
            const input = $(this).find('input');

            // Remover event listeners antigos
            $(input).off('input');

            // Aplicar máscara de acordo com o tipo
            switch (tipo) {
                case 'Telefone':
                    $(input).on('input', function () {
                        let value = $(this).val().replace(/\D/g, '');
                        if (value.length > 10) {
                            value = value.slice(0, 10);
                        }
                        // Aplicar máscara enquanto o usuário digita
                        $(this).val(formatPhone(value));
                    });
                    break;
                case 'WhatsApp':
                    $(input).on('input', function () {
                        let value = $(this).val().replace(/\D/g, ''); // Remove caracteres não numéricos
                        if (value.length > 11) {
                            value = value.slice(0, 11); // Limita o comprimento a 255 caracteres
                        }
                        // Aplicar máscara enquanto o usuário digita
                        $(this).val(formatPhone(value));
                    });
                    break;
                case 'Email':
                    $(input).on('input', function () {
                        // Validação simples para e-mail
                        let value = $(this).val();
                        if (value.length > 255) {
                            value = value.slice(0, 255); // Limita o comprimento a 255 caracteres
                        }
                        $(this).val(value);
                    });
                    break;
            }
        });
    }

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

    // Aplicar máscaras quando a tabela é atualizada
    $('#contatos-table').on('change', 'select', function () {
        applyMasks();
    });

    // Aplicar máscaras quando a página carrega
    applyMasks();
});