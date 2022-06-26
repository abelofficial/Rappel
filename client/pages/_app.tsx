import { NextUIProvider } from "@nextui-org/react";
import Layout from "../components/Layout";
import { AuthProvider } from "../Contexts/Auth";

function MyApp({ Component, pageProps, fallback }: any) {
  return (
    // 2. Use at the root of your app
    <NextUIProvider>
      <AuthProvider>
        <Layout>
          <Component {...pageProps} />
        </Layout>
      </AuthProvider>
    </NextUIProvider>
  );
}

export default MyApp;
