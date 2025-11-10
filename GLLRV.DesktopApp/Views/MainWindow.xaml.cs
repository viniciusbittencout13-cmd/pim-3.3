private void CarregarUsuarioNoTopo()
{
    if (_usuario == null) return;

    UserNameText.Text = _usuario.NomeCompleto ?? _usuario.Username ?? "-";
    UserLevelText.Text = $"TÉCNICO: {_usuario.Nivel ?? "-"}";
    UserCategoryText.Text = $"CATEGORIA: {_usuario.Categoria ?? "-"}";

    var nome = _usuario.NomeCompleto ?? _usuario.Username ?? "";
    var partes = nome.Split(' ', System.StringSplitOptions.RemoveEmptyEntries);

    string iniciais = "?";
    if (partes.Length >= 2)
        iniciais = $"{partes[0][0]}{partes[1][0]}";
    else if (partes.Length == 1)
        iniciais = partes[0][0].ToString();

    InitialsText.Text = iniciais.ToUpperInvariant();
}
