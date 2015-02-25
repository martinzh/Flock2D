# Este script acepta entrada de la linea de comandos
# 1 -> tamanio de paso
# 2 -> repeticiones
# 3 -> incremento en el valor del campo
# 4 -> campo inicial
# 5 -> campo final
# 6 -> grafica : 1 -> vel 0 -> dr2

import sys
  
nombre = 'corridas.sh'
script = open(nombre,'w')

start = int(sys.argv[1])
stop  = int(sys.argv[2])
step  = int(sys.argv[3])
T     = sys.argv[4]
rate  = sys.argv[5]
procs = float(sys.argv[6])

N     = int((stop - start)/step)

print(N)

script.write("#!/bin/bash\n")

eta = "0.1"
ro = "1080"

# for i in range(N):

#   if i%(procs) == 0:
#     script.write( "./Flock2D.exe " + repr(start + round(i*step,7)) + " " + eta + " " + ro + " " + T + " " + l + " " + ht +  " " + ht + " " + rate + "\n" )
#   else:
#     script.write( "nohup ./Flock2D.exe " + repr(start + round(i*step,7)) + " " + eta + " " + ro + " " + T + " " + l + " " + ht +  " " + ht + " " + rate + " &\n" )

for i in range(N):

  if i%(procs) == 0:
    script.write( "(time mono Flock2D.exe " + repr(i) + " " + eta + " " + ro + " " + T + " " + rate + ") 2>> tiempo.dat \n" )
  else:
    script.write( "(time nohup mono Flock2D.exe " + repr(i) + " " + eta + " " + ro + " " + T + " " + rate + " &) 2>> tiempo.dat\n" )



script.close()
