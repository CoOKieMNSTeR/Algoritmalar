#!/usr/bin/python
# -*- coding: utf-8 -*-
import random
import sys

def ortak_bln(a, b):
    while b != 0:
        a, b = b, a % b
    return a


def ters_crpm(e, p):
    d = 0
    x1 = 0
    x2 = 1
    y1 = 1
    temp_p = p

    while e > 0:
        temp1 = int(temp_p / e)
        temp2 = int(temp_p - (temp1 * e))
        temp_p = e
        e = temp2

        x = x2- temp1* x1
        y = d - temp1 * y1
    
        x2 = x1
        x1 = x
        d = y1
        y1 = y
    
    if temp_p == 1:
        return d + p


def asal_knt(num):
    if num == 2:
        return True
    if num < 2 or num % 2 == 0:
        return False
    for n in range(3, int(num**0.5)+2, 2):
        if num % n == 0:
            return False
    return True

def anahtar(p, q):
    if not (asal_knt(p) and asal_knt(q)):
        print('Her iki sayi da asal olmalidir.')
        sys.exit("Hata");
    elif p == q:
        print('p ve q ayni olmamalidir')
        sys.exit("Hata");

    n = p * q


    t = (p-1) * (q-1)

    e = random.randrange(1, t)

    g = ortak_bln(e, t)
    while g != 1:
        e = random.randrange(1, t)
        g = ortak_bln(e, t)


    d = ters_crpm(e, t)

    return ((e, n), (d, n))

def sifrele(pk, orj):

    key, n = pk

    sfrlnms = [(ord(char) ** key) % n for char in orj]

    return sfrlnms

def sifre_coz(pk, sifrelenmis):

    key, n = pk
    
    mtnorj = [chr((char ** key) % n) for char in sifrelenmis]
    return ''.join(mtnorj)
    

if __name__ == '__main__':
   
    print ("RSA Sifreleyici ve Cözücü")
    p = int( input("P yi giriniz (17, 19, 23, vb): "))
    q = int( input("Q yu giriniz (Yukarıda girdiginizden farkli): "))
    print ("Herkese açık / özel anahtar çiftlerinizi şimdi oluşturuluyor . . .")
    public, private = anahtar(p, q)
    print ("Genel anahtarınız ", public," ve özel anahtarınız ", private)
    mesaj = input("Özel anahtarınızla şifrelemek için bir mesaj girin: ")
    sifrele_msg = sifrele(private, mesaj)
    print ("Şifrelenmiş mesajınız: ")
    print (''.join(map(lambda x: str(x), sifrele_msg)))
    print ("Açık anahtarla iletilen: ", public ," . . .")
    print ("Sizin Mesajınız:")
    print (sifre_coz(public, sifrele_msg))