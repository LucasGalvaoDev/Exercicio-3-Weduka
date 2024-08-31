$(document).ready(function () {
    $('.toggle-sidebar').on('click', function () {
        $('.sidebar').toggleClass('active');
        $('.main-content').toggleClass('shifted');
        $('.top-navbar').toggleClass('shifted');
        $('.toggle-sidebar').toggleClass('shifted');
    });

    window.editPerson = function (id) {
        window.location.href = `/Home/EditPessoa/${id}`;
    };

    window.deletePerson = function (id) {
        if (confirm('Tem certeza que deseja deletar esta pessoa?')) {
            $.ajax({
                url: `/api/pessoas/${id}`,
                method: 'DELETE',
                success: function () {
                    alert('Pessoa deletada com sucesso!');
                    location.reload();
                },
                error: function () {
                    alert('Erro ao deletar a pessoa.');
                }
            });
        }
    };
});
