
[![Donate](https://img.shields.io/badge/Donate-PayPal-orange.svg)](https://www.paypal.me/rauldose)

# furlanSQL
SQL par Furlan 

![alt text](https://upload.wikimedia.org/wikipedia/commons/thumb/8/87/Bandiere_dal_Fri%C3%BBl.svg/492px-Bandiere_dal_Fri%C3%BBl.svg.png)

inspired by [GomorraSql](https://github.com/aurasphere/gomorra-sql)

## Set up
furlanSQL can be used either as a C# library or as a standalone SQL database client.

## Language reference
Follows a table that roughly maps FurlanSQL language to standard SQL:

| furlanSQL keyword              | SQL equivalent    | Valid in...            |
|--------------------------------|-------------------|------------------------|
| cjape su                       | SELECT            | SELECT                 |
| fa e disfa                     | UPDATE            | UPDATE                 |
| pare                           | INSERT            | INSERT                 |
| dentri                         | INTO              | INSERT                 |
| are vie                        | DELETE            | DELETE                 |
| di rif o di raf                | INNER JOIN        | SELECT                 |
| dal cjossul                    | FROM              | SELECT, DELETE         |
| dut                            | *                 | SELECT                 |
| dula                           | WHERE             | SELECT, UPDATE, DELETE |
| e                              | AND               | ANY WHERE CLAUSE       |
| o                              | OR                | ANY WHERE CLAUSE       |
| nie                            | NULL              | ANY WHERE CLAUSE       |
| al e                           | IS                | ANY WHERE CLAUSE       |
| nol e                          | IS NOT            | ANY WHERE CLAUSE       |
| chist                          | VALUES            | INSERT                 |
| cussi                          | SET               | UPDATE                 |
| come                           | = (assignment)    | UPDATE                 |
| >                              | >                 | ANY WHERE CLAUSE       |
| <                              | <                 | ANY WHERE CLAUSE       |
| = (comparison)                 | = (comparison)    | ANY WHERE CLAUSE       |
| !=                             | !=                | ANY WHERE CLAUSE       |
| <>                             | <>                | ANY WHERE CLAUSE       |
| <=                             | <=                | ANY WHERE CLAUSE       |
| >=                             | >=                | ANY WHERE CLAUSE       |
| taconiti                       | ROLLBACK          | TRANSACTION            |
| daur man                       | COMMIT            | TRANSACTION            |
| tache bande                    | BEGIN TRANSACTION | TRANSACTION            |

## Supported Database
furlanSQL has been quickly tested with SQLite in memory. Other databases may not work properly.

## Contributions
Improvements are always appreciated! If you want to contribute to this project though, remember to open an issue with your suggestion before doing any changes. This will help you avoid working on something that won't get merged.

## Project status
This project is considered a starting point and I hope it will be further developed.

## Contacts
You can contact me using my account e-mail or opening an issue on this repo. I'll try to reply ASAP.

## License
The project is released under the MIT license, which lets you reuse the code for any purpose you want (even commercial) with the only requirement being copying this project license on your project.

<sub>Copyright (c) 2021 Raul Dose</sub>
