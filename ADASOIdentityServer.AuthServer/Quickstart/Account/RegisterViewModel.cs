﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace IdentityServerHost.Quickstart.UI;

public class RegisterViewModel: RegisterInputModel
{
    public string ReturnUrl { get; set; }
    public bool AllowRememberLogin { get; set; } = true;
    public bool EnableLocalLogin { get; set; } = true;
    public IEnumerable<ExternalProvider> ExternalProviders { get; set; }
        = Enumerable.Empty<ExternalProvider>();
    public IEnumerable<ExternalProvider> VisibleExternalProviders
        => ExternalProviders.Where(x => !string.IsNullOrWhiteSpace(x.DisplayName));
    public bool IsExternalLoginOnly
        => EnableLocalLogin == false && ExternalProviders?.Count() == 1;
    public string ExternalLoginScheme
        => IsExternalLoginOnly ? ExternalProviders?.SingleOrDefault()?.AuthenticationScheme : null;
}
