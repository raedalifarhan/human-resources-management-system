import type { Metadata } from "next";
import { Inter } from "next/font/google";
import "./globals.css";
import NextAuthSessionProvider from "@/providers/NextAuthSessionProvider";
import ToasterProvider from "@/providers/ToasterProvider";
import Navbar from "@/components/nav/Navbar";

const inter = Inter({ subsets: ["latin"] });

export const metadata: Metadata = {
  title: "Create Next App",
  description: "Generated by create next app",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="ar" dir='rtl'>
      <body className={inter.className} suppressHydrationWarning={true}>
      <ToasterProvider />
        <NextAuthSessionProvider>
        <Navbar />
          <main className='container mx-auto px-5 pt-10'>
            {children}
          </main>
        </NextAuthSessionProvider>
      </body>
    </html>
  );
}
