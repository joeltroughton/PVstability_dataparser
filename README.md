# PVstability_dataparser
A quick and dirty script to recover statistics from raw J-V photovoltaic stability data.

Input folder structure should look something like this. Each folder corresponds to a measured pixel. Each .dat file is a J-V curve measurement. The number after COM*_ corresponds to the elapsed time in hours.
```markdown
📦data
 ┣ 📂0
  ┗ 📜COM5_0.000_0.dat
  ┗ 📜COM5_1.000_0.dat
  ┗ 📜COM5_2.000_0.dat
  ┗ 📜...
  ┗ 📜COM5_1000.0_0.dat
 ┣ 📂1
  ┗ 📜COM5_0.000_1.dat
  ┗ 📜COM5_1.000_1.dat
  ┗ 📜COM5_2.000_1.dat
  ┗ 📜...
  ┗ 📜COM5_1000.0_1.dat
 ┣ 📂2
  ┗ 📜COM5_0.000_2.dat
  ┗ 📜COM5_1.000_2.dat
  ┗ 📜COM5_2.000_2.dat
  ┗ 📜...
  ┗ 📜COM5_1000.0_2.dat
```

The root data directory is defined in the `searchdir` string. Example of each .dat file to be processed:

```
2/25/2021	5:04 PM
Notes = 

Sweep Parameters
Sweep Down
NPLC = 1.00000E+0
Compliance = 2.00000E-1 A 
Delay = 1.00000E-3 S 
Auto Zero Off
Autorange On
Temp A = 0.000000
Temp B = 0.000000

Figures of Merit 
Device Area = 0.104000 cm2
Jsc (mA/cm2)	-1.35359E+1
Voc (V)	1.19242E+0
Max Power(mW/cm2)	-1.00309E+1
FF (%)	6.21478E-1
RR= 	5.79740E-1 
Rs (Ohms-cm2) 	6.07639E+2
Rsh (Ohms-cm2)	6.07639E+2
Impp	-1.00309E+1


Voltage	Current1		
1.30000E+0	2.37546E+1
1.25000E+0	9.83256E+0
1.20000E+0	5.30642E-1
1.15000E+0	-4.71636E+0
```


Output will be:

```markdown
📦data
 ┣ 📂completed
   ┗ 📜0_report.dat
   ┗ 📜1_report.dat
   ┗ 📜2_report.dat

```

With the format:
```
File Name   Jsc(mA / cm2)    Voc(V) Pmax(mW / cm2)   Fill Factor Rect.Rato Rseries(Ohms)  Rshunt(Ohms)
COM5_0.022_0.dat	-1.27761E+1	1.13200E+0	-1.05220E+1	7.27531E-1
COM5_0.191_0.dat	-1.22039E+1	1.14082E+0	-9.86092E+0	7.08274E-1
COM5_0.359_0.dat	-1.18372E+1	1.14726E+0	-9.53171E+0	7.01877E-1
COM5_0.528_0.dat	-1.14961E+1	1.15380E+0	-9.22606E+0	6.95564E-1
COM5_0.696_0.dat	-1.11551E+1	1.15993E+0	-8.94033E+0	6.90952E-1
```


