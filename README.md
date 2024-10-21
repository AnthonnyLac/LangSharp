

# LangSharp

## Visão Geral

O projeto **LangSharp** inicialmente tinha como objetivo integrar o **IronPython** para execução de scripts Python dentro da aplicação. No entanto, após uma avaliação das necessidades de compatibilidade, ficou claro que o **IronPython** não oferece suporte para versões do Python superiores à 2.7. Como o projeto requer suporte para as funcionalidades modernas do Python, decidimos trocar para o **Python.NET**, que é compatível com versões 3.x do Python, incluindo a mais recente, **3.11.7**.

O **Python.NET** proporciona uma integração robusta entre C# e Python, permitindo que integremos um interpretador Python diretamente na aplicação .NET e chamemos código Python a partir da aplicação C#. Essa solução está mais alinhada com os objetivos do projeto, garantindo a compatibilidade com versões mais recentes do Python, ao mesmo tempo que mantém a interação fluida com C#.

## Por que Python.NET?

As principais razões para a troca do **IronPython** para o **Python.NET** incluem:
- **Compatibilidade**: O Python.NET suporta versões 3.x do Python, enquanto o IronPython está limitado à versão 2.x.
- **Funcionalidades Modernas**: O Python.NET permite o uso de funcionalidades e bibliotecas modernas do Python, que não são compatíveis com o IronPython.

