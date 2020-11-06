using STN.Cobrancas.Data.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace STN.Cobrancas.Tests.UnitTests
{
    public class CpfUnitTest
    {
        [Fact]
        public void IsCpf()
        {
            var cpf = "63955700054";

            var result = CpfService.IsCpf(cpf);

            Assert.True(result);
        }

        [Fact]
        public void IsCpf_Formatado()
        {
            var cpf = "639.557.000-54";

            var result = CpfService.IsCpf(cpf);

            Assert.True(result);
        }

        [Fact]
        public void IsCpf_ReturnFalse()
        {
            var cpf = "6395570005D";

            var result = CpfService.IsCpf(cpf);

            Assert.False(result);
        }
    }
}
