 Math Training 
 ===
Training with basic math operators ('+','-','*','/');

Made with Andularjs, ASP.NET CORE 2.0 and PostgreSQL Entity Framework Core provider

### Note ###
* This repository uses [Sebreiro/sebreiro.github.io](https://github.com/Sebreiro/sebreiro.github.io) repository as a git submodule  
* [Sebreiro/sebreiro.github.io](https://github.com/Sebreiro/sebreiro.github.io) is the Math Training frontend and used as a github page ([sebreiro.github.io](https://sebreiro.github.io))  
* ASP.NET backend used to collect training statistic (only for authenticated users )

Installation
---
1. Use  `git clone --recursive` to clone repository with submodule (it's won't work if you download repository as a ZIP)
2. Set Web as default start up project
3. Change PostgreSQL connection string in appsettings.json
4. Change `window.__environment.enableBackend = false;` in `environment.js` to `true`, if your want to collect and use training statistic

Featured Libraries
---
### Frontend ###
* [Angularjs](https://github.com/angular/angular.js)
* [bootstrap](https://github.com/twbs/bootstrap)
* [fontAwesome](https://github.com/FortAwesome/Font-Awesome)
* [c3.js](https://github.com/c3js/c3) - chart library
* [lodash](https://github.com/lodash/lodash)
### Backend ###
* [ASP.NET CORE 2.0](https://github.com/aspnet/Home)
* [Entity Framework Core with PostgreSQL provider](http://www.npgsql.org/efcore/index.html)
* [NLog](https://github.com/NLog)
* [Autofac](https://github.com/autofac/Autofac)

Screenshots
---

![Math training settings](img/MathTrainingSettings_4.png?raw=true "Math training settings")
![Math training addition correct](img/AdditionTraining_Correct.png?raw=true "Math training addition correct")
![Math training multiplication incorrect](img/MultiplicationTraining_Incorrect.png?raw=true "Math training multiplication incorrect")
