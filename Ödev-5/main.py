# coding: utf-8
import sys
sys.stdout.write('.')

# Kodlanmış kodu görüntüle
def KelGoster(text, tab):
    for l in text:
        i = next(index for (index, d) in enumerate(tab) if d["harf"] == l)
        sys.stdout.write( tab[i]["binary"] )
    print ("")

# Çeviri tablosunu döndürür: İkili kodla ilişkilendirilen her harf
def TabloGet(node, tab):
    if "sağ" not in node:
        tab.append({"harf": node["harf"], "binary": node["binary"]})
    else:
        TabloGet(node["sağ"], tab)
        TabloGet(node["sol"], tab)


#   Ağacın yapraklarını görüntüler: harf ve ilgili kodu
def Goster(node):
    if "sağ" not in node:
        print "harf: " + node["harf"] + " = " + node["binary"]
    else:
        Goster(node["sağ"])
        Goster(node["sol"])

##
#   Harfleri kodla
def kodla(node, code):
    node["binary"] = code
    if "sol" in node:
        kodla(node["sol"], code + "0")
    if "sağ" in node:
        kodla(node["sağ"], code + "1")
    return


#   Çocuk düğüm harflerinin oluşma sayısına göre sıralanmış bir nesne listesi oluşturun.
def setList(text, oc):
    for l in text:
        if {"harf": l, "count": text.count(l), "binary" : None} not in oc:
            oc.append({"harf": l, "count": text.count(l), "binary" : None})
    oc = sorted(oc, key=lambda k: k['count'])
    oc.reverse()

def main():
    print ("Sıkıştırılacak metni girin:")
    text = raw_input()
    oc = []

    setList(text, oc)
    # Ağacı oluşturun, en düşük ağırlık düğümlerini bir araya getirin
    while len(oc) != 1:
        m_node = {"binary" : None, "count" : None, "sol": None, "sağ": None}
        m_node["sol"] = oc.pop()
        m_node["sağ"] = oc.pop()
        m_node["count"] = m_node["sol"]["count"] + m_node["sağ"]["count"]
        oc.append(m_node)
        oc = sorted(oc,key=lambda k: k['count'])
        oc.reverse()

    root = oc.pop()
    kodla(root["sol"], "0")
    kodla(root["sağ"], "1")
    #Goster(root)
    tab = []
    TabloGet(root, tab)
    print (tab)
    KelGoster(text, tab)

if __name__ == "__main__":
    main()
