using FinancialSummaryApi.V1.Boundary.Response;
using System;
using System.Text;

namespace FinancialSummaryApi.V1.UseCase.Helpers
{
    public static class TemplateGenerator
    {
        public static string GetHTMLReportString1()
        {

            var sb = new StringBuilder();
            sb.Clear();
            sb.Append(@"<html>
<head>

</head>

<body style='margin-top: 0px;margin-left: 0px;'>
<div id='page_1' style='position: relative;overflow: hidden;margin: 15px 0px 35px 53px;padding: 0px;border: none;width: 742px;height: 1073px;'>
<div id='p1dimg1' style='position: absolute;top: 0px;left: 0px;z-index: -1;width: 684px;height: 1073px;'>
<img src='data:image/png;base64,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wLTx2Yvan3XmWxdPUWx9XKHVKJs3x6jFoO71waoA16nJG4+51bN18+ZpETC+w27UWDe3ejc+1q9as2bVmrV1adLiaX9mSxz98/eo9erExvKueSyOeutl+kfs5GaD49FhtcSvPP/JX7nsohjMjBbHiy3tO/blKxqWZvwdjxBcXJcpYzAj5nubGliTjn708cpMGa02vnywDXYnylpEb632OOSJj1z6zLUwJq5EtNIWm2z6drT33qNN/SmMK62vvEhy6fPL/1nqVpdQlui9X3z5paUMapTn/PYjxk+z3VtXhrNrogw+89XP91Hb/uRF7f3G++yzsDfutwmwh00+HjDo5Z63u+PHvv7ViXu3rURttbWFS1765t171h7ltf/y/uPf+6bxf0zW2uPufb+4BlO47OBAepl6NHrLY8cPL4+5X6xeF1POkowPa9daWqtl01Vr51ad9sdP/sPTXhIRfQdbkVGrdbDdQMKIiKj1R1dtvNGarXbNx/cmGpWttvXLlyOUUSmDuv1FdLtkPANPjYtrucnxf/SDU942cSjideSQmxz4he9/v0078bPQautt2wscdqJHXLXpyoU2GtbBSff7g+d86F2bW4vetpqhubdRqSe+6+/6mnUTfwv6aOG8Z71quP2VhwA3OJM7YFT68x9y1C+97BkxcTh9WbyycPcOmfbeS/Q+s/xHdsIB77p4bfqUL1/Xli4Ve+zpr4x162Nh6qn3WqJftfGnDjzwcfe639N+66Hj49YPe8NLe4npt05otY/alIlsex187cILbnTLw1d+chARpVx4yeXf/tEPJyyuxLDU1ftMnk9wpaW7963YIpYSvffWIuqPy+gl//6BE3/1ATtdzrXl1251h3/63HnThoqMTn3XqLWr1SW9jxZqHfbFE0Klx+aNl8fsmm1OANVee28v+fC76mAwedLD0cLB6/a7mq8G4HppcgeUUo44/DaLU7uXGtFrL1v2X1u0nZ1Z34Gli+6WH7/NH/oetbQWcdWmtjCqwx3cSe8am7aNqbX22qOd9okPx8xslLryXjqL8+ZGrSXucMBNzn71qcv341kcU1ZKLxNmu1teetu8KQbDic9eS3ntJ/7pLrc8psbyNIVR+qj1wf/396/8p8+eO+GygNIiBqcfdezkl7J4u78etfQNGwZr1/XR1pf2L94nsC+0/pS3vG5lB2x/2cK16zfvcLe/eM9b+5TfpVqjLs6IvHhUauXFGFNWazhcHIBSIyJqueet7vDxb38jyqhGXX7Vi1czbh8BvZeIXupf/MZDduUeSwA3AJPPYS/+9d9w5fK2areuStstS6fYj/q1+5ZB3TtTuPRoJUrUwXgQwzaD0WqJiFb6gevWn33yqb33snRL4hpltHiKfQer3Q7a74Dp58zLmz76kRKDLdMXRkQZ1Bof/PSn2/gWQdt89BoLmx9wu7tPXF5Z/LaIUeunndmvuGLijRPHzzRYveb8C78XS2t/XQ+Ru9PNDqkbN9QpP+L/vuDbfX5htOJXtGz3j+31rc8Hnf64P482ilZ3aR6MpXGlz3zIkSIASGL6IPMer/7D44exPKHbntse1x6x6aq/PfKE2PHFctehvnhD3cF49Nl2RyxKrG7llEccv7jVqUvXz0V870eXxPi0ydTrHeu5z3ptj8nnGlrto7nB3LEPqlFWNlB57AMGa2cnLy7q/uvX77923cSvjqLX3uvm+f6ad/dR+/zJr526GS3RF9rPnXzc+Rd/r/UW1/2PvI/ioXe/57SRDXc7+fiFmeFg66sJesQoRv/ylc+9++xPnnHOWWecc9Z7zv3U8scZZ3/qnZ/5xMLmLe/tYTc64A4HHrhLl54uXZ0ZmzeOD0JdK0MmAX7CTZtPvpVSj/2N+x33jtfE7HY3hr9u9dpG73nic2LlFH57WCm119jympeuWF/+eo+Ng3jxGW994M/dqZalrUwtPeIOzz5mcbdy2yluFo0iDtx37fo6vHziO9p61DIazB7+5496+R8ee9efud0nv3De4/7h9bFqdjQeNbH9I6KfdtSTRqPJe7xlFG0QpfeIVgb1Nje/+aHr9vnWZZdu/521lVZ6lHr7E4/sf/svscPd7mtFH/RTHvsnf3/CoyZ+tc6uutHRD77o9H+cK1vOC0TvwzK471+dFKu3vboyIqLEzMaNm95w5pan6HHa0X92xAtPjJ3dPHhxWsPeDtxn/fiKWFcMAhlM++NYI2LU27uOf/pgoY+vEtxDeqwdDB9wh7usXJM9rbW2fElj7bHtZX5lfCHfWRd+8znvfnsZtVEf7z/HoU89+tKFpestt70UbfE/BxHRy7nPeX3ML0Rst2UvJXq01r5+xYbfPe0lNzvxqIe88eWXbLwyWt122PzS4gebNj34DnedtvPaa0QfX1xQo/Xe+7nPeFXpPbab6WBx/EePsnbfB5/63B1fVHmtqFEOGK6OTfNlvDJbv8DW2hXrBquOfuA7z/7kZZsX+ig++93vPOzVzy+PfUBZs+LQyHKk9T7byruf8ryV73sv/a6HHhajTTv9NVo881XKqx52fJ14PyeAG6Id3V9uMBg85I53PXDdmu9v2tSn3Bfn2jEewT4ek1jjSy96cxuNBtNuoLcHLF/6vzBfYy7KNjPdLO7ol1Ke/dF3vvaTH7nToT/z3Yu/+9nvfHNmbnXEcPJee4mIGI1G4wf+9P43+vXb3ubjX/3aqEZZeZOD5cH8438MaoznXNp+17T18TT4H3vqSxZ2cTLBWnpr6+fm+vzmmF3VYvJ0ir3395zz6ahl0q18rmUL0Y/+tfu88T8/2sa/AFsrvfT16x722heNequ9xXCmlYhVq1dexllG0UuPWgdR9l0184Bb33HlatcoUeJ+dz7iQ587b2cTPfZoERs3/MHdfulaf5kAP7F2sps0ivrdl7ylb9qwZ+49WKPf+9DDD1q9uu7FCBivSY+I+IXb3LaV3srkO+G0KLXXCzdd/s/n//dnf/C9OrNqvreYNjte9Ihy8eWXL8+Q9+EnvaBt2DDXSlu5sV1xRV9d+ckJ73+JErdat/6XDv+ZXb/SvdZaa/3mC04vfWH6QIEeM7M3P/GRe2BsRo04/RHHv/lRT4jtoqT0XnqP3kYztc4M28zM8gjH8QmCxX/XGN+ZeLRp44V//ba+3USNPeKDxzxjp4c3SuvDUp72O3+0Bw6EAPzk2EkHDFrvvb/x/zux9PEdX5b2THtM3+DtuvHB98UB7cM2WDUYfeTEF+zCA3f41Cu3JtP/pu94QuFWWkT80xOeXTbOR48+KLVvPzVebz1itDTz3fi4eq3D4XCbcCiL1xC2qzZctbyCJWLz6Wdu2ryhbhmTt9X2funmBeOvLI1YXPrGQak3GQ6/8sK/rYu34pmeAj22mWD50ANucrv9Dyrb32cvIiJqLzX6BRuvesun/rMtjCKW7i88YcnLL3uasuV3ZsUnlh8yXvmHH/Grs/ObynDls5S+fFykL74ViyfsW49Sy/geDdGj915q37DhnJNfs3Rof+ueGA9ELDv5XS9RF/qGZz3o4bs5XTbA9dNOOqDXUko58s6/8sR7/mZtJVoMrsUDAyuG39VWFjZffsUrzxi10Y4nDuoRU2+DuGLdxoPt+7Rv3PGebu/jKXf2qbOnHPX4MlqI8Yn27Td5ZekFlGgRMws9Lr9yNL8p+mDlW9uX/ycGZXndopXW2hvef6t99lseijA+37DdUIDxFrGNt3y1l+FC3O3wW1740r9vbTw7QB9ND5taom99/eV8jM5+9ql98/w25THWFs+D1IveEoYAABhQSURBVCNf/4JYnO9oB+F1NX8fxi99607qJTa+7sy77HdQaePJGxZvGz0+4rJlxfryInrvLWrE+EzSxit/eMo7bn3QgbFdBPSIqKVElPn5HXdrq33/4dpBqXtwLAzA3reTP3nLO4wv+cPH9YUro5bR0uj4a2Hy9R6xdGva1ja30z44GvVB2W5A3NaPKDsYyF2Wj1Usbg+mXXZYps31N1bL4s2QShx7919/ye89OmJ8b4Gy7V0DVsxdPyhlfv6q/33VP8TGqyK2nnJh8VElZocf/urn++JefS219ogvPe91t7/xQRGj1tt4coJtb63Ulyd0KlFqb/N3OvigTzzxBRHjmXailDJtJ7b20iJK3+rSuZkYzAzro+9+jy3L3+rpxrcIKn3Nqps/7ejFz0xUxvdpuJq2u//ToNQS8emTT/nAE54eG65qtUeU0luNvvLK/1rGR4/G70OprZbeB23hKy94w/6r51bFhCsIltet7+zMSe3xyoce20d7bqYMgJ8EOxonGMuHdAe1RIxec+YbP/Gvf/zmV9aZ2bbTXepdVGqNfsCwXPiq9/YYDQY7mT1w3CWD3g5ev8/076plflO0He3XrRnOHrR+fal12jD7NpwdL6nMzJ7w6w960n0ectIZb/mrD72jD4ZbXYFWSumjvnHzrQ+++Ref97rWWq31nne425cv/G5f8RYt3fmw9j561Ktf8P2XvSUioo+iDKK3Xsvnnv3qiHbl/OgOJx3zzR9dGMOZLVPeliijiD7qC5sOO+im5zz71HXDmdFofJu/1vvifRB6W5j4npQepZQ2t2n7CfLe9NgT//kpRw0Gg+2PnQyWMq+0+sPNGw5av2+Zm5vwRkeZdmxmpg4OXrdvGUTUEq0vL3zccNtPDzU+5X+/2/yf9rr39VJf/OF3/MU73xyllDrsdbHw2viKzjaKzRsOXnfA8x786D++5316H/U+2OZ+RltNvVDGM0AOyw4Pa7TeH3mPe47/uXeuUgHYG8quT9g3/kt9weWXH/IXjy59GGXUTj1jN55yvJxXfuyDT/yH02NQjjvi117+6OPqwqgOd3WwW9vuXrcTvqe1Ov2S8d77TqcoWl7C+C0ajUbD4fBrF1/08S+d+5ULv/OjDVesnZk7/MY3/YO73PPAfdaNp+BdPIrQdnKx+g6evY9aGdSLNl71zs/817cu+M4lG67Yd999f/bGN/39O91j37WrRjF1FMCOX2+0Hq3Fdu/wrsyeOz9aGNbBtG/rEWXSz6NH9EmrtNNn7EsnZUop8z3O+963P3zup79/8Q83toUb77ffrW/8U3c77La3OfjgiBj1Nuhl+6deiLjLyX9SB733qFEuGW385ne+F7MTOmbpBUT0iM0b2+veVwYKAMhlJ8cDVioRvZSfWr/+vKe9/C7PfML87NV47PZmeon5DY/+pfue8ujjSut9ONj1Gd3HJ7x38j07mzdmB4sYn55eXsJ4rQbDYbR+y/1vdNgv3nv8yeUFLN68py7dorD0FhFt8stZvAPB9BXvvd941Zpjfuk3tlrB8aax92mJsXJ9tvp8j6ill8WDOhO+uvwqpqzTzGAYU96t3nuNpSGk231t2iqNDw9MPWWzdEKntTYT5c43PeTONz1k+fMtRrUt3jOhRumTbkM0jPjchd9t41GQvcyUGnOrok++SLL06KVE6Xe9xWFGCAIJXb1t+fgv9M8dfOim17/nWe97++49ZYkWpa5bvfqy152xvs4ujuRaceh4lxZyja9pGx+qn7aUCYsfb+mXvtK3Xo2V6zPezE/cRMXOJukrgzae8Ge5KmK8ixzRS6krP7PNqPip++u97ODtWtEG01Zs/C5NXEJZHtI/7UsTTeyG7VctxhM2j7XFpGl1PEygTX2Dl65lGSw+ar736FHLxGs/YzyBUemjtx3/tPGbuuNhqgA3MFfjvMC1qy+Nu9srz84N2+D//v7U2ynFOG2Wh8D2GuWYX7j3Kx/++L05dRXAXnKNju1fEwqAvWbxlMHikY6+YcOrH3Xs3gpigL1rr3UA7E09YjwvUSlffN7rdnCWAeCGTQeQ0PgOBL32tl8vP3PTn+q7cgkKwA2RDuCGY8UQv8Vj/ltNd9h7lDqM0lprdeFet7z1vz75Bb30HlGnTZwMcEOnA7jhKNFai15r33B52X5KgzoYrJr7+Zve8veO+OX/e8Rv7bN2TZS2s9sOANzA7bXrBWCvGE981KLsdIIJgAz8LeQGZRSxo7tMLt3QqdbFuxXtodUC+EnleAAA5OV4AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXsPlfx1++OF7cT2AvetrX/va3l4FYC8ovfe9vQ4AwN7hvAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB5DXfwtVLKHlsPuIHpve/tVQDYuR11QPhbBrtFQwPXF84LAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADIa7jjL5dS9sx6AAB7Xum97+11AAD2DucFACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHntUgeUUq7r9QAA9rBSSum97/Sb9szaAAB72M47ICJKKYcddtgeWBsAYI/5+te/bnwAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkJcOAIC8dAAA5KUDACAvHQAAeekAAMhLBwBAXjoAAPLSAQCQlw4AgLx0AADkpQMAIC8dAAB56QAAyEsHAEBeOgAA8tIBAJCXDgCAvHQAAOSlAwAgLx0AAHnpAADISwcAQF46AADy0gEAkFfpve/kO0rZM6sCAOxh/z+Uo7XCv/UcCAAAAABJRU5ErkJggg==' id='p1img1' style='width: 684px;height: 1073px;'></div>


<div class='dclr' style='clear: both;float: none;height: 1px;margin: 0px;padding: 0px;overflow: hidden;'></div>
<div id='id1_1' style='border: none;margin: 79px 0px 0px 4px;padding: 0px;width: 738px;overflow: hidden;'>
<p class='p0 ft0' style='font: bold 13px 'Arial';color: #222222;line-height: 16px;text-align: left;margin-top: 0px;margin-bottom: 0px;'>Income Services, Hackney Service Centre, 1 Hillman Street, London E8 1DY</p>
<p class='p1 ft0' style='font: bold 13px 'Arial';color: #222222;line-height: 16px;text-align: left;margin-top: 1px;margin-bottom: 0px;'>Tel: 0208 356 3100 24 hour payment line: 0208 356 5050</p>
<p class='p0 ft1' style='font: bold 13px 'Arial';line-height: 16px;text-align: left;margin-top: 0px;margin-bottom: 0px;'><nobr><a href='http://www.hackney.gov.uk/your-rent'>www.hackney.gov.uk/your-rent</a></nobr></p>
<p class='p2 ft2' style='font: 13px 'Arial';color: #222222;line-height: 16px;text-align: left;padding-left: 504px;margin-top: 3px;margin-bottom: 0px;'>Date: 17 November 2021</p>
<p class='p3 ft2' style='font: 13px 'Arial';color: #222222;line-height: 16px;text-align: left;padding-left: 507px;margin-top: 2px;margin-bottom: 0px;'>Payment : xxxxxxxxx</p>
<p class='p4 ft3' style='font: 13px 'Arial';line-height: 16px;text-align: left;padding-left: 13px;margin-top: 10px;margin-bottom: 0px;'>Name</p>
<p class='p5 ft3' style='font: 13px 'Arial';line-height: 16px;text-align: left;padding-left: 13px;margin-top: 2px;margin-bottom: 0px;'>Address1</p>
<p class='p5 ft3' style='font: 13px 'Arial';line-height: 16px;text-align: left;padding-left: 13px;margin-top: 2px;margin-bottom: 0px;'>Address2</p>
<p class='p5 ft3' style='font: 13px 'Arial';line-height: 16px;text-align: left;padding-left: 13px;margin-top: 2px;margin-bottom: 0px;'>Address3</p>
<p class='p5 ft3' style='font: 13px 'Arial';line-height: 16px;text-align: left;padding-left: 13px;margin-top: 2px;margin-bottom: 0px;'>Postcode</p>
<p class='p6 ft4' style='font: bold 29px 'Arial';line-height: 34px;text-align: left;padding-left: 100px;margin-top: 24px;margin-bottom: 0px;'>STATEMENT OF YOUR ACCOUNT</p>
<p class='p7 ft5' style='font: bold 15px 'Arial';line-height: 18px;text-align: left;padding-left: 145px;margin-top: 7px;margin-bottom: 0px;'>for the period 27 September 2021 to 17 November 2021</p>
<table cellpadding='0' cellspacing='0' class='t0' style='width: 670px;margin-left: 4px;margin-top: 13px;font: 12px 'Arial';'>
<tr>
	<td colspan='2' class='tr0 td0' style='border-left: #000000 1px solid;border-right: #000000 1px solid;border-top: #000000 1px solid;border-bottom: #cccccc 1px solid;padding: 0px;margin: 0px;width: 113px;vertical-align: bottom;background: #cccccc;height: 23px;'><p class='p8 ft6' style='font: bold 12px 'Arial';line-height: 15px;text-align: left;padding-left: 45px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Date</p></td>
	<td class='tr0 td1' style='border-right: #000000 1px solid;border-top: #000000 1px solid;border-bottom: #cccccc 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;background: #cccccc;height: 23px;'><p class='p9 ft6' style='font: bold 12px 'Arial';line-height: 15px;text-align: left;padding-left: 49px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Transaction Details</p></td>
	<td class='tr0 td2' style='border-top: #000000 1px solid;border-bottom: #cccccc 1px solid;padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;background: #cccccc;height: 23px;'><p class='p10 ft6' style='font: bold 12px 'Arial';line-height: 15px;text-align: left;padding-left: 46px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Debit</p></td>
	<td class='tr0 td3' style='border-right: #000000 1px solid;border-top: #000000 1px solid;border-bottom: #cccccc 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;background: #cccccc;height: 23px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr0 td4' style='border-right: #000000 1px solid;border-top: #000000 1px solid;border-bottom: #cccccc 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;background: #cccccc;height: 23px;'><p class='p12 ft6' style='font: bold 12px 'Arial';line-height: 15px;text-align: left;padding-left: 34px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Credit</p></td>
	<td class='tr0 td5' style='border-right: #000000 1px solid;border-top: #000000 1px solid;border-bottom: #cccccc 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;background: #cccccc;height: 23px;'><p class='p13 ft6' style='font: bold 12px 'Arial';line-height: 15px;text-align: left;padding-left: 37px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Balance</p></td>
</tr>
<tr>
	<td class='tr1 td6' style='border-left: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 60px;vertical-align: bottom;height: 20px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr1 td7' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 53px;vertical-align: bottom;height: 20px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr1 td8' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;height: 20px;'><p class='p14 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: left;padding-left: 1px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Balance brought forward</p></td>
	<td class='tr1 td9' style='border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;height: 20px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr1 td10' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;height: 20px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr1 td11' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;height: 20px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr1 td12' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;height: 20px;'><p class='p15 ft6' style='font: bold 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>£1,113.31</p></td>
</tr>
<tr>
	<td colspan='2' class='tr2 td13' style='border-left: #000000 1px solid;border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 113px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td14' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td15' style='border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td16' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td17' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td18' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
</tr>
<tr>
	<td colspan='2' class='tr3 td19' style='border-left: #000000 1px solid;border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 113px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p16 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 1px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>27 Sep 2021</p></td>
	<td class='tr3 td20' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p17 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: left;padding-left: 3px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Weekly Charge</p></td>
	<td class='tr3 td21' style='border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr3 td22' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p18 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 3px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>£98.30</p></td>
	<td class='tr3 td23' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr3 td24' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p15 ft6' style='font: bold 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>£1,211.61</p></td>
</tr>
<tr>
	<td colspan='2' class='tr1 td25' style='border-left: #000000 1px solid;border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 113px;vertical-align: bottom;height: 20px;'><p class='p16 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 1px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>27 Sep 2021</p></td>
	<td class='tr1 td8' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;height: 20px;'><p class='p17 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: left;padding-left: 3px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Housing Benefit</p></td>
	<td class='tr1 td9' style='border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;height: 20px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr1 td10' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;height: 20px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr1 td11' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;height: 20px;'><p class='p15 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'><nobr>-£98.30</nobr></p></td>
	<td class='tr1 td12' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;height: 20px;'><p class='p15 ft6' style='font: bold 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>£1,113.31</p></td>
</tr>
<tr>
	<td colspan='2' class='tr2 td13' style='border-left: #000000 1px solid;border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 113px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td14' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td15' style='border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td16' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td17' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td18' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
</tr>
<tr>
	<td colspan='2' class='tr4 td26' style='border-left: #000000 1px solid;border-right: #000000 1px solid;padding: 0px;margin: 0px;width: 113px;vertical-align: bottom;height: 19px;'><p class='p16 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 1px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>28 Sep 2021</p></td>
	<td class='tr4 td27' style='border-right: #000000 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;height: 19px;'><p class='p17 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: left;padding-left: 3px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Bank Payment</p></td>
	<td class='tr4 td28' style='padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;height: 19px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr4 td29' style='border-right: #000000 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;height: 19px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr4 td30' style='border-right: #000000 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;height: 19px;'><p class='p15 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'><nobr>-£15.00</nobr></p></td>
	<td class='tr4 td31' style='border-right: #000000 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;height: 19px;'><p class='p15 ft6' style='font: bold 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>£1,098.31</p></td>
</tr>
<tr>
	<td class='tr2 td32' style='border-left: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 60px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td33' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 53px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td14' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td15' style='border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td16' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td17' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td18' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
</tr>
<tr>
	<td class='tr3 td34' style='border-left: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 60px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p19 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>04</p></td>
	<td class='tr3 td35' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 53px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p15 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Oct 2021</p></td>
	<td class='tr3 td20' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p17 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: left;padding-left: 3px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Weekly Charge</p></td>
	<td class='tr3 td21' style='border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr3 td22' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p18 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 3px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>£98.30</p></td>
	<td class='tr3 td23' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr3 td24' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p15 ft6' style='font: bold 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>£1,196.61</p></td>
</tr>
<tr>
	<td class='tr1 td6' style='border-left: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 60px;vertical-align: bottom;height: 20px;'><p class='p19 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>04</p></td>
	<td class='tr1 td7' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 53px;vertical-align: bottom;height: 20px;'><p class='p15 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Oct 2021</p></td>
	<td class='tr1 td8' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;height: 20px;'><p class='p17 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: left;padding-left: 3px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Housing Benefit</p></td>
	<td class='tr1 td9' style='border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;height: 20px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr1 td10' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;height: 20px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr1 td11' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;height: 20px;'><p class='p15 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'><nobr>-£98.30</nobr></p></td>
	<td class='tr1 td12' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;height: 20px;'><p class='p15 ft6' style='font: bold 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>£1,098.31</p></td>
</tr>
<tr>
	<td class='tr2 td32' style='border-left: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 60px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td33' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 53px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td14' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td15' style='border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td16' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td17' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td18' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
</tr>
<tr>
	<td class='tr4 td36' style='border-left: #000000 1px solid;padding: 0px;margin: 0px;width: 60px;vertical-align: bottom;height: 19px;'><p class='p19 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>05</p></td>
	<td class='tr4 td37' style='border-right: #000000 1px solid;padding: 0px;margin: 0px;width: 53px;vertical-align: bottom;height: 19px;'><p class='p15 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Oct 2021</p></td>
	<td class='tr4 td27' style='border-right: #000000 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;height: 19px;'><p class='p17 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: left;padding-left: 3px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Bank Payment</p></td>
	<td class='tr4 td28' style='padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;height: 19px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr4 td29' style='border-right: #000000 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;height: 19px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr4 td30' style='border-right: #000000 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;height: 19px;'><p class='p15 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'><nobr>-£15.00</nobr></p></td>
	<td class='tr4 td31' style='border-right: #000000 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;height: 19px;'><p class='p15 ft6' style='font: bold 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>£1,083.31</p></td>
</tr>
<tr>
	<td class='tr2 td32' style='border-left: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 60px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td33' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 53px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td14' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td15' style='border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td16' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td17' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td18' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
</tr>
<tr>
	<td class='tr3 td34' style='border-left: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 60px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p19 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>11</p></td>
	<td class='tr3 td35' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 53px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p15 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Oct 2021</p></td>
	<td class='tr3 td20' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p17 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: left;padding-left: 3px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Weekly Charge</p></td>
	<td class='tr3 td21' style='border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr3 td22' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p18 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 3px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>£98.30</p></td>
	<td class='tr3 td23' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr3 td24' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p15 ft6' style='font: bold 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>£1,181.61</p></td>
</tr>
<tr>
	<td class='tr1 td6' style='border-left: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 60px;vertical-align: bottom;height: 20px;'><p class='p19 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>11</p></td>
	<td class='tr1 td7' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 53px;vertical-align: bottom;height: 20px;'><p class='p15 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Oct 2021</p></td>
	<td class='tr1 td8' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;height: 20px;'><p class='p17 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: left;padding-left: 3px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Housing Benefit</p></td>
	<td class='tr1 td9' style='border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;height: 20px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr1 td10' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;height: 20px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr1 td11' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;height: 20px;'><p class='p15 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'><nobr>-£98.30</nobr></p></td>
	<td class='tr1 td12' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;height: 20px;'><p class='p15 ft6' style='font: bold 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>£1,083.31</p></td>
</tr>
<tr>
	<td class='tr2 td32' style='border-left: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 60px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td33' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 53px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td14' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td15' style='border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td16' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td17' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td18' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
</tr>
<tr>
	<td class='tr4 td36' style='border-left: #000000 1px solid;padding: 0px;margin: 0px;width: 60px;vertical-align: bottom;height: 19px;'><p class='p19 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>12</p></td>
	<td class='tr4 td37' style='border-right: #000000 1px solid;padding: 0px;margin: 0px;width: 53px;vertical-align: bottom;height: 19px;'><p class='p15 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Oct 2021</p></td>
	<td class='tr4 td27' style='border-right: #000000 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;height: 19px;'><p class='p17 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: left;padding-left: 3px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Bank Payment</p></td>
	<td class='tr4 td28' style='padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;height: 19px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr4 td29' style='border-right: #000000 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;height: 19px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr4 td30' style='border-right: #000000 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;height: 19px;'><p class='p15 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'><nobr>-£15.00</nobr></p></td>
	<td class='tr4 td31' style='border-right: #000000 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;height: 19px;'><p class='p15 ft6' style='font: bold 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>£1,068.31</p></td>
</tr>
<tr>
	<td class='tr2 td32' style='border-left: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 60px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td33' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 53px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td14' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td15' style='border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td16' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td17' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td18' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
</tr>
<tr>
	<td class='tr3 td34' style='border-left: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 60px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p19 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>18</p></td>
	<td class='tr3 td35' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 53px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p15 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Oct 2021</p></td>
	<td class='tr3 td20' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p17 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: left;padding-left: 3px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Weekly Charge</p></td>
	<td class='tr3 td21' style='border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr3 td22' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p18 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 3px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>£98.30</p></td>
	<td class='tr3 td23' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr3 td24' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p15 ft6' style='font: bold 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>£1,166.61</p></td>
</tr>
<tr>
	<td class='tr1 td6' style='border-left: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 60px;vertical-align: bottom;height: 20px;'><p class='p19 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>18</p></td>
	<td class='tr1 td7' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 53px;vertical-align: bottom;height: 20px;'><p class='p15 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Oct 2021</p></td>
	<td class='tr1 td8' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;height: 20px;'><p class='p17 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: left;padding-left: 3px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Housing Benefit</p></td>
	<td class='tr1 td9' style='border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;height: 20px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr1 td10' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;height: 20px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr1 td11' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;height: 20px;'><p class='p15 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'><nobr>-£98.30</nobr></p></td>
	<td class='tr1 td12' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;height: 20px;'><p class='p15 ft6' style='font: bold 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>£1,068.31</p></td>
</tr>
<tr>
	<td class='tr2 td32' style='border-left: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 60px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td33' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 53px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td14' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td15' style='border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td16' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td17' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td18' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
</tr>
<tr>
	<td class='tr4 td36' style='border-left: #000000 1px solid;padding: 0px;margin: 0px;width: 60px;vertical-align: bottom;height: 19px;'><p class='p19 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>19</p></td>
	<td class='tr4 td37' style='border-right: #000000 1px solid;padding: 0px;margin: 0px;width: 53px;vertical-align: bottom;height: 19px;'><p class='p15 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Oct 2021</p></td>
	<td class='tr4 td27' style='border-right: #000000 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;height: 19px;'><p class='p17 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: left;padding-left: 3px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Bank Payment</p></td>
	<td class='tr4 td28' style='padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;height: 19px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr4 td29' style='border-right: #000000 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;height: 19px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr4 td30' style='border-right: #000000 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;height: 19px;'><p class='p15 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'><nobr>-£15.00</nobr></p></td>
	<td class='tr4 td31' style='border-right: #000000 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;height: 19px;'><p class='p15 ft6' style='font: bold 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>£1,053.31</p></td>
</tr>
<tr>
	<td class='tr2 td32' style='border-left: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 60px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td33' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 53px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td14' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td15' style='border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td16' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td17' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td18' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
</tr>
<tr>
	<td class='tr3 td34' style='border-left: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 60px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p19 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>25</p></td>
	<td class='tr3 td35' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 53px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p15 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Oct 2021</p></td>
	<td class='tr3 td20' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p17 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: left;padding-left: 3px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Weekly Charge</p></td>
	<td class='tr3 td21' style='border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr3 td22' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p18 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 3px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>£98.30</p></td>
	<td class='tr3 td23' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr3 td24' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p15 ft6' style='font: bold 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>£1,151.61</p></td>
</tr>
<tr>
	<td class='tr1 td6' style='border-left: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 60px;vertical-align: bottom;height: 20px;'><p class='p19 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>25</p></td>
	<td class='tr1 td7' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 53px;vertical-align: bottom;height: 20px;'><p class='p15 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Oct 2021</p></td>
	<td class='tr1 td8' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;height: 20px;'><p class='p17 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: left;padding-left: 3px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Housing Benefit</p></td>
	<td class='tr1 td9' style='border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;height: 20px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr1 td10' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;height: 20px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr1 td11' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;height: 20px;'><p class='p15 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'><nobr>-£98.30</nobr></p></td>
	<td class='tr1 td12' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;height: 20px;'><p class='p15 ft6' style='font: bold 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>£1,053.31</p></td>
</tr>
<tr>
	<td class='tr2 td32' style='border-left: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 60px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td33' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 53px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td14' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td15' style='border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td16' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td17' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td18' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
</tr>
<tr>
	<td class='tr4 td36' style='border-left: #000000 1px solid;padding: 0px;margin: 0px;width: 60px;vertical-align: bottom;height: 19px;'><p class='p19 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>26</p></td>
	<td class='tr4 td37' style='border-right: #000000 1px solid;padding: 0px;margin: 0px;width: 53px;vertical-align: bottom;height: 19px;'><p class='p15 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Oct 2021</p></td>
	<td class='tr4 td27' style='border-right: #000000 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;height: 19px;'><p class='p17 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: left;padding-left: 3px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Bank Payment</p></td>
	<td class='tr4 td28' style='padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;height: 19px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr4 td29' style='border-right: #000000 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;height: 19px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr4 td30' style='border-right: #000000 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;height: 19px;'><p class='p15 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'><nobr>-£15.00</nobr></p></td>
	<td class='tr4 td31' style='border-right: #000000 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;height: 19px;'><p class='p15 ft6' style='font: bold 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>£1,038.31</p></td>
</tr>
<tr>
	<td colspan='2' class='tr2 td13' style='border-left: #000000 1px solid;border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 113px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td14' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td15' style='border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td16' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td17' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td18' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
</tr>
<tr>
	<td colspan='2' class='tr3 td19' style='border-left: #000000 1px solid;border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 113px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p16 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 1px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>01 Nov 2021</p></td>
	<td class='tr3 td20' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p17 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: left;padding-left: 3px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Weekly Charge</p></td>
	<td class='tr3 td21' style='border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr3 td22' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p18 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 3px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>£98.30</p></td>
	<td class='tr3 td23' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr3 td24' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p15 ft6' style='font: bold 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>£1,136.61</p></td>
</tr>
<tr>
	<td colspan='2' class='tr1 td25' style='border-left: #000000 1px solid;border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 113px;vertical-align: bottom;height: 20px;'><p class='p16 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 1px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>01 Nov 2021</p></td>
	<td class='tr1 td8' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;height: 20px;'><p class='p17 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: left;padding-left: 3px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Housing Benefit</p></td>
	<td class='tr1 td9' style='border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;height: 20px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr1 td10' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;height: 20px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr1 td11' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;height: 20px;'><p class='p15 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'><nobr>-£98.30</nobr></p></td>
	<td class='tr1 td12' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;height: 20px;'><p class='p15 ft6' style='font: bold 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>£1,038.31</p></td>
</tr>
<tr>
	<td colspan='2' class='tr2 td13' style='border-left: #000000 1px solid;border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 113px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td14' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td15' style='border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td16' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td17' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td18' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
</tr>
<tr>
	<td colspan='2' class='tr4 td26' style='border-left: #000000 1px solid;border-right: #000000 1px solid;padding: 0px;margin: 0px;width: 113px;vertical-align: bottom;height: 19px;'><p class='p16 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 1px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>02 Nov 2021</p></td>
	<td class='tr4 td27' style='border-right: #000000 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;height: 19px;'><p class='p17 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: left;padding-left: 3px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Bank Payment</p></td>
	<td class='tr4 td28' style='padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;height: 19px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr4 td29' style='border-right: #000000 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;height: 19px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr4 td30' style='border-right: #000000 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;height: 19px;'><p class='p15 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'><nobr>-£15.00</nobr></p></td>
	<td class='tr4 td31' style='border-right: #000000 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;height: 19px;'><p class='p15 ft6' style='font: bold 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>£1,023.31</p></td>
</tr>
<tr>
	<td colspan='2' class='tr2 td13' style='border-left: #000000 1px solid;border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 113px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td14' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td15' style='border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td16' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td17' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td18' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
</tr>
<tr>
	<td colspan='2' class='tr3 td19' style='border-left: #000000 1px solid;border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 113px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p16 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 1px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>08 Nov 2021</p></td>
	<td class='tr3 td20' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p17 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: left;padding-left: 3px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Weekly Charge</p></td>
	<td class='tr3 td21' style='border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr3 td22' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p18 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 3px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>£98.30</p></td>
	<td class='tr3 td23' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr3 td24' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p15 ft6' style='font: bold 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>£1,121.61</p></td>
</tr>
<tr>
	<td colspan='2' class='tr1 td25' style='border-left: #000000 1px solid;border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 113px;vertical-align: bottom;height: 20px;'><p class='p16 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 1px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>08 Nov 2021</p></td>
	<td class='tr1 td8' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;height: 20px;'><p class='p17 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: left;padding-left: 3px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Housing Benefit</p></td>
	<td class='tr1 td9' style='border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;height: 20px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr1 td10' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;height: 20px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr1 td11' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;height: 20px;'><p class='p15 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'><nobr>-£98.30</nobr></p></td>
	<td class='tr1 td12' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;height: 20px;'><p class='p15 ft6' style='font: bold 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>£1,023.31</p></td>
</tr>
<tr>
	<td colspan='2' class='tr2 td13' style='border-left: #000000 1px solid;border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 113px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td14' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td15' style='border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td16' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td17' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td18' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
</tr>
<tr>
	<td colspan='2' class='tr4 td26' style='border-left: #000000 1px solid;border-right: #000000 1px solid;padding: 0px;margin: 0px;width: 113px;vertical-align: bottom;height: 19px;'><p class='p16 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 1px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>09 Nov 2021</p></td>
	<td class='tr4 td27' style='border-right: #000000 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;height: 19px;'><p class='p17 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: left;padding-left: 3px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Bank Payment</p></td>
	<td class='tr4 td28' style='padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;height: 19px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr4 td29' style='border-right: #000000 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;height: 19px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr4 td30' style='border-right: #000000 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;height: 19px;'><p class='p15 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'><nobr>-£15.00</nobr></p></td>
	<td class='tr4 td31' style='border-right: #000000 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;height: 19px;'><p class='p15 ft6' style='font: bold 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>£1,008.31</p></td>
</tr>
<tr>
	<td colspan='2' class='tr2 td13' style='border-left: #000000 1px solid;border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 113px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td14' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td15' style='border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td16' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td17' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td18' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
</tr>
<tr>
	<td colspan='2' class='tr3 td19' style='border-left: #000000 1px solid;border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 113px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p16 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 1px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>15 Nov 2021</p></td>
	<td class='tr3 td20' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p17 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: left;padding-left: 3px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Weekly Charge</p></td>
	<td class='tr3 td21' style='border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr3 td22' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p18 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 3px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>£98.30</p></td>
	<td class='tr3 td23' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr3 td24' style='border-right: #000000 1px solid;border-bottom: #d9d9d9 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;background: #d9d9d9;height: 21px;'><p class='p15 ft6' style='font: bold 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>£1,106.61</p></td>
</tr>
<tr>
	<td colspan='2' class='tr1 td25' style='border-left: #000000 1px solid;border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 113px;vertical-align: bottom;height: 20px;'><p class='p16 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 1px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>15 Nov 2021</p></td>
	<td class='tr1 td8' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;height: 20px;'><p class='p17 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: left;padding-left: 3px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>Housing Benefit</p></td>
	<td class='tr1 td9' style='border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;height: 20px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr1 td10' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;height: 20px;'><p class='p11 ft7' style='font: 1px 'Arial';line-height: 1px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr1 td11' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;height: 20px;'><p class='p15 ft8' style='font: 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'><nobr>-£98.30</nobr></p></td>
	<td class='tr1 td12' style='border-right: #000000 1px solid;border-top: #000000 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;height: 20px;'><p class='p15 ft6' style='font: bold 12px 'Arial';line-height: 15px;text-align: right;padding-right: 2px;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>£1,008.31</p></td>
</tr>
<tr>
	<td class='tr2 td32' style='border-left: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 60px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td33' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 53px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td14' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 207px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td15' style='border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 79px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td16' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 43px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td17' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 104px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
	<td class='tr2 td18' style='border-right: #000000 1px solid;border-bottom: #000000 1px solid;padding: 0px;margin: 0px;width: 119px;vertical-align: bottom;height: 4px;'><p class='p11 ft9' style='font: 1px 'Arial';line-height: 4px;text-align: left;margin-top: 0px;margin-bottom: 0px;white-space: nowrap;'>&nbsp;</p></td>
</tr>
</table>
<p class='p20 ft10' style='font: bold 17px 'Arial';line-height: 19px;text-align: left;padding-left: 56px;margin-top: 2px;margin-bottom: 0px;'>As of 17 November 2021 your account balance was £1008.31 in arrears.</p>
</div>
<div id='id1_2' style='border: none;margin: 39px 0px 0px 19px;padding: 0px;width: 652px;overflow: hidden;'>
<p class='p0 ft11' style='font: bold 12px 'Arial';color: #ffffff;line-height: 15px;text-align: left;margin-top: 0px;margin-bottom: 0px;'>As your landlord, the council has a duty to make sure all charges are paid up to date. This is because the housing income goes toward the upkeep of council housing and providing services for residents. You must make weekly charges payment a priority. If you don’t pay, you risk losing your home.</p>
</div>
</div>
<div id='page_2' style='position: relative;overflow: hidden;margin: 1123px 0px 0px 0px;padding: 0px;border: none;width: 0px;height: 0px;'>


</div>
</body>
</html>
");

            return sb.ToString();
        }
        public static string GetHTMLReportString(ExportResponse report)
        {
            var date = DateTime.Today.ToString("D");
            var sb = new StringBuilder();
            sb.Clear();
            sb.Append(@"<html><head>
<style>
body {
    margin-top: 0px;
    margin-left: 0px;
}

#page_1 {
    position: relative;
    overflow: hidden;
    margin: 15px 0px 35px 53px;
    padding: 0px;
    border: none;
    width: 800px;
    height: auto;
}

    #page_1 #id1_1 {
        border: none;
        margin: 79px 0px 0px 4px;
        padding: 0px;
        border: none;
        width: 654px;
    }

    #page_1 #id1_2 {
        margin: 39px 0px 0px 19px;
        padding: 0px;
        border: none;
        width: 652px;
        background: #000000;
        color: #fff;
    }

    #page_1 #p1dimg1 {
        position: absolute;
        top: 0px;
        left: 0px;
        z-index: -1;
        width: auto;
        height: auto;
    }

        #page_1 #p1dimg1 #p1img1 {
            width: auto;
            height: auto;
        }




#page_2 {
    position: relative;
    overflow: hidden;
    margin: 1123px 0px 0px 0px;
    padding: 0px;
    border: none;
    width: 0px;
    height: 0px;
}





.dclr {
    clear: both;
    float: none;
    height: 1px;
    margin: 0px;
    padding: 0px;
    overflow: hidden;
}

.ft0 {
    font: bold 13px 'Arial';
    color: #222222;
    line-height: 16px;
}

.ft1 {
    font: bold 13px 'Arial';
    line-height: 16px;
}

.ft2 {
    font: 13px 'Arial';
    color: #222222;
    line-height: 16px;
}

.ft3 {
    font: 13px 'Arial';
    line-height: 16px;
}

.ft4 {
    font: bold 29px 'Arial';
    line-height: 34px;
}

.ft5 {
    font: bold 15px 'Arial';
    line-height: 18px;
}

.ft6 {
    font: bold 12px 'Arial';
    line-height: 15px;
}

.ft7 {
    font: 1px 'Arial';
    line-height: 1px;
}

.ft8 {
    font: 12px 'Arial';
    line-height: 15px;
}

.ft9 {
    font: 1px 'Arial';
    line-height: 4px;
}

.ft10 {
    font: bold 17px 'Arial';
    line-height: 19px;
}

.ft11 {
    font: bold 12px 'Arial';
    color: #ffffff;
    line-height: 15px;
}

.p0 {
    text-align: left;
    margin-top: 0px;
    margin-bottom: 0px;
}

.p1 {
    text-align: left;
    margin-top: 1px;
    margin-bottom: 0px;
}

.p2 {
    text-align: left;
    padding-left: 504px;
    margin-top: 3px;
    margin-bottom: 0px;
}

.p3 {
    text-align: left;
    padding-left: 507px;
    margin-top: 2px;
    margin-bottom: 0px;
}

.p4 {
    text-align: left;
    padding-left: 13px;
    margin-top: 10px;
    margin-bottom: 0px;
}

.p5 {
    text-align: left;
    padding-left: 13px;
    margin-top: 2px;
    margin-bottom: 0px;
}

.p6 {
    text-align: left;
    padding-left: 100px;
    margin-top: 24px;
    margin-bottom: 0px;
}

.p7 {
    text-align: left;
    padding-left: 145px;
    margin-top: 7px;
    margin-bottom: 0px;
}

.p8 {
    text-align: left;
    padding-left: 45px;
    margin-top: 0px;
    margin-bottom: 0px;
    white-space: nowrap;
}

.p9 {
    text-align: left;
    padding-left: 49px;
    margin-top: 0px;
    margin-bottom: 0px;
    white-space: nowrap;
}

.p10 {
    text-align: left;
    padding-left: 46px;
    margin-top: 0px;
    margin-bottom: 0px;
    white-space: nowrap;
}

.p11 {
    text-align: left;
    margin-top: 0px;
    margin-bottom: 0px;
    white-space: nowrap;
}

.p12 {
    text-align: left;
    padding-left: 34px;
    margin-top: 0px;
    margin-bottom: 0px;
    white-space: nowrap;
}

.p13 {
    text-align: left;
    padding-left: 37px;
    margin-top: 0px;
    margin-bottom: 0px;
    white-space: nowrap;
}

.p14 {
    text-align: left;
    padding-left: 1px;
    margin-top: 0px;
    margin-bottom: 0px;
    white-space: nowrap;
}

.p15 {
    text-align: right;
    padding-right: 2px;
    margin-top: 0px;
    margin-bottom: 0px;
    white-space: nowrap;
}

.p16 {
    text-align: right;
    padding-right: 1px;
    margin-top: 0px;
    margin-bottom: 0px;
    white-space: nowrap;
}

.p17 {
    text-align: left;
    padding-left: 3px;
    margin-top: 0px;
    margin-bottom: 0px;
    white-space: nowrap;
}

.p18 {
    text-align: right;
    padding-right: 3px;
    margin-top: 0px;
    margin-bottom: 0px;
    white-space: nowrap;
}

.p19 {
    text-align: right;
    margin-top: 0px;
    margin-bottom: 0px;
    white-space: nowrap;
}

.p20 {
    text-align: left;
    padding-left: 56px;
    margin-top: 2px;
    margin-bottom: 0px;
}

.td0 {
    border-left: #000000 1px solid;
    border-right: #000000 1px solid;
    border-top: #000000 1px solid;
    border-bottom: #cccccc 1px solid;
    padding: 0px;
    margin: 0px;
    width: 113px;
    vertical-align: bottom;
    background: #cccccc;
}

.td1 {
    border-right: #000000 1px solid;
    border-top: #000000 1px solid;
    border-bottom: #cccccc 1px solid;
    padding: 0px;
    margin: 0px;
    width: 207px;
    vertical-align: bottom;
    background: #cccccc;
}

.td2 {
    border-top: #000000 1px solid;
    border-bottom: #cccccc 1px solid;
    padding: 0px;
    margin: 0px;
    width: 79px;
    vertical-align: bottom;
    background: #cccccc;
}

.td3 {
    border-right: #000000 1px solid;
    border-top: #000000 1px solid;
    border-bottom: #cccccc 1px solid;
    padding: 0px;
    margin: 0px;
    width: 43px;
    vertical-align: bottom;
    background: #cccccc;
}

.td4 {
    border-right: #000000 1px solid;
    border-top: #000000 1px solid;
    border-bottom: #cccccc 1px solid;
    padding: 0px;
    margin: 0px;
    width: 104px;
    vertical-align: bottom;
    background: #cccccc;
}

.td5 {
    border-right: #000000 1px solid;
    border-top: #000000 1px solid;
    border-bottom: #cccccc 1px solid;
    padding: 0px;
    margin: 0px;
    width: 119px;
    vertical-align: bottom;
    background: #cccccc;
}

.td6 {
    border-left: #000000 1px solid;
    border-top: #000000 1px solid;
    padding: 0px;
    margin: 0px;
    width: 60px;
    vertical-align: bottom;
}

.td7 {
    border-right: #000000 1px solid;
    border-top: #000000 1px solid;
    padding: 0px;
    margin: 0px;
    width: 53px;
    vertical-align: bottom;
}

.td8 {
    border-right: #000000 1px solid;
    border-top: #000000 1px solid;
    padding: 0px;
    margin: 0px;
    width: 207px;
    vertical-align: bottom;
}

.td9 {
    border-top: #000000 1px solid;
    padding: 0px;
    margin: 0px;
    width: 79px;
    vertical-align: bottom;
}

.td10 {
    border-right: #000000 1px solid;
    border-top: #000000 1px solid;
    padding: 0px;
    margin: 0px;
    width: 43px;
    vertical-align: bottom;
}

.td11 {
    border-right: #000000 1px solid;
    border-top: #000000 1px solid;
    padding: 0px;
    margin: 0px;
    width: 104px;
    vertical-align: bottom;
}

.td12 {
    border-right: #000000 1px solid;
    border-top: #000000 1px solid;
    padding: 0px;
    margin: 0px;
    width: 119px;
    vertical-align: bottom;
}

.td13 {
    border-left: #000000 1px solid;
    border-right: #000000 1px solid;
    border-bottom: #000000 1px solid;
    padding: 0px;
    margin: 0px;
    width: 113px;
    vertical-align: bottom;
}

.td14 {
    border-right: #000000 1px solid;
    border-bottom: #000000 1px solid;
    padding: 0px;
    margin: 0px;
    width: 207px;
    vertical-align: bottom;
}

.td15 {
    border-bottom: #000000 1px solid;
    padding: 0px;
    margin: 0px;
    width: 79px;
    vertical-align: bottom;
}

.td16 {
    border-right: #000000 1px solid;
    border-bottom: #000000 1px solid;
    padding: 0px;
    margin: 0px;
    width: 43px;
    vertical-align: bottom;
}

.td17 {
    border-right: #000000 1px solid;
    border-bottom: #000000 1px solid;
    padding: 0px;
    margin: 0px;
    width: 104px;
    vertical-align: bottom;
}

.td18 {
    border-right: #000000 1px solid;
    border-bottom: #000000 1px solid;
    padding: 0px;
    margin: 0px;
    width: 119px;
    vertical-align: bottom;
}

.td19 {
    border-left: #000000 1px solid;
    border-right: #000000 1px solid;
    border-bottom: #d9d9d9 1px solid;
    padding: 0px;
    margin: 0px;
    width: 113px;
    vertical-align: bottom;
    background: #d9d9d9;
}


.tr0 {
    height: 23px;
}

.tr1 {
    height: 20px;
}

.tr2 {
    height: 4px;
}

.tr3 {
    height: 21px;
}

.tr4 {
    height: 19px;
}

.t0 {
    width: 670px;
    margin-left: 4px;
    margin-top: 13px;
    font: 12px 'Arial';
}

td, th {
    border: #000000 1px solid !important;
    border-bottom: #d9d9d9 1px solid !important;
    padding: 0px;
    margin: 0px;
    vertical-align: bottom;
}

p {
    display: block;
    margin-block-start: 1em;
    margin-block-end: 1em;
    margin-inline-start: 0px;
    margin-inline-end: 0px;
}

div {
    display: block;
}

.address {
    border: 2px solid gray;
    width: 250px;
    height: 130px;
}

tr:nth-child(even) {
    background: #d9d9d9;
}

</style>
</head>
            <body>
    <div id='page_1'>
    <div id='p1dimg1'>
    <img src='data:image/png;base64,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' id='p1img1'></div>


            <div class='dclr'></div>
            <div id='id1_1'>
            <p class='p0 ft0'>Income Services, Hackney Service Centre, 1 Hillman Street, London E8 1DY</p>
            <p class='p1 ft0'>Tel: 0208 356 3100 24 hour payment line: 0208 356 5050</p>
            <p class='p0 ft1'><nobr><a href='http://www.hackney.gov.uk/your-rent'>www.hackney.gov.uk/your-rent</a></nobr><hr></p>");
            sb.AppendFormat(@"
            <p class='p2 ft2'>Date: {0}</p>
            <p class='p3 ft2'>Payment : xxxxxxxxx</p>
            <div class='address'>
            <p class='p4 ft3'>Name</p>
            <p class='p5 ft3'>Address1</p>
            <p class='p5 ft3'>Address2</p>
            <p class='p5 ft3'>Address3</p>
            <p class='p5 ft3'>Postcode</p>
            </div>
            <p class='p6 ft4'>STATEMENT OF YOUR ACCOUNT</p>
            <p class='p7 ft5'>for the period {1}</p>
            <table cellpadding='0' cellspacing='0' class='t0'>
            <tbody>
            <tr>
	            <td colspan='2' class='tr0 td0'><p class='p8 ft6'>Date</p></td>
	            <td class='tr0 td1'><p class='p9 ft6'>Transaction Details</p></td>
	            <td class='tr0 td2'><p class='p10 ft6'>Debit</p></td>
	            <td class='tr0 td4'><p class='p12 ft6'>Credit</p></td>
	            <td class='tr0 td5'><p class='p13 ft6'>Balance</p></td>
            </tr>
            <tr>
	            <td colspan='2' class='tr1 td6'><p class='p16 ft8'>&nbsp;</p></td>
	            <td class='tr1 td8'><p class='p17 ft8'>Balance brought forward</p></td>
	            <td class='tr1 td9'><p class='p11 ft7'>&nbsp;</p></td>
	            <td class='tr1 td11'><p class='p15 ft8'>&nbsp;</p></td>
	            <td class='tr1 td12'><p class='p15 ft6'>{2}</p></td>
            </tr>", date, report.StatementPeriod, report.BalanceBroughtForward);
            foreach (var item in report.Data)
            {
                sb.AppendFormat(@"
				            <tr>
	            <td colspan='2' class='tr1 td25'><p class='p16 ft8'>{0}</p></td>
	            <td class='tr1 td8'><p class='p17 ft8'>{1}</p></td>
	            <td class='tr1 td10'><p class='p11 ft8'>{2}</p></td>
	            <td class='tr1 td11'><p class='p15 ft8'><nobr>{3}</nobr></p></td>
	            <td class='tr1 td12'><p class='p15 ft6'>{4}</p></td>
            </tr>", item.Date, item.TransactionDetail, item.Debit, item.Credit, item.Balance);
            };

            sb.AppendFormat(@"
            </tbody>
            </table>
            <p class='p20 ft10'>As of {0} your account balance was {1} in arrears.</p>
            </div>", date, report.Balance);
            sb.Append(@"
            <div id='id1_2'>
            <p class='p0 ft11'>As your landlord, the council has a duty to make sure all charges are paid up to date. This is because the housing income goes toward the upkeep of council housing and providing services for residents. You must make weekly charges payment a priority. If you don’t pay, you risk losing your home.</p>
            </div>
            </div>

            </body></html>");

            return sb.ToString();
        }
    }
}
