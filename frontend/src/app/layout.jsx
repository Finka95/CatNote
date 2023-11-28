import { Inter } from 'next/font/google'
import './globals.css'
import { UserProvider } from '@auth0/nextjs-auth0/client';

const inter = Inter({ subsets: ['latin'] })

export const metadata = {
  title: 'CatNote',
}

// const providerConfig = {
//   domain: "dev-ujxvenaomkidb36h.us.auth0.com",
//   clientId: "knfixJWkPdQbohcN2J6DEf5osSxY157q",
//   onRedirectCallback,
//   authorizationParams: {
//     redirect_uri: window.location.origin,
//     audience: "https://localhost:7165/api"
//   },
// };

export default function RootLayout({ children }) {
  return (
    <html lang="en">
      <UserProvider>
      <body className={inter.className}>
          {children}
      </body>
      </UserProvider>
    </html>
  )
}
