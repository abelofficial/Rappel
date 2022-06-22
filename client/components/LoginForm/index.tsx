import React from "react";
import { Form, Formik } from "formik";
import {
  initialValues,
  validationSchema,
  fieldNames,
  fieldLabels,
} from "./loginProps";
import { LoginUserRequest } from "../../types";
import { Button, Card, Grid, useTheme } from "@nextui-org/react";
import TextField from "../TextField";
import { loginUserCommand } from "../../services/commands";
import { useRouter } from "next/router";

const Index = () => {
  const { theme } = useTheme();
  const router = useRouter();

  const onSubmitHandler = async (values: LoginUserRequest) => {
    try {
      var result = await loginUserCommand(values);
      console.log("Result: ", result.data.token);
      router.push("/");
    } catch (e: any) {
      console.log("Error: ", e?.response?.data);
    }
  };
  return (
    <Formik
      initialValues={initialValues}
      onSubmit={onSubmitHandler}
      validationSchema={validationSchema}
    >
      {({ isSubmitting }) => (
        <Form>
          <Card variant='bordered'>
            <Grid.Container
              gap={1}
              justify='center'
              alignItems='center'
              css={{
                backgroundColor: theme?.colors.primaryShadow,
                border: theme?.borderWeights.light,
                boxShadow: theme?.shadows.lg,
                padding: theme?.space.lg,
              }}
            >
              <TextField
                type='text'
                name={fieldNames.username}
                label={fieldLabels.username}
                fullWidth
              />
              <TextField
                type='password'
                name={fieldNames.password}
                label={fieldLabels.password}
                fullWidth
              />

              <Grid xs={12}>
                <Button type='submit' color='success' disabled={isSubmitting}>
                  Login
                </Button>
              </Grid>
            </Grid.Container>
          </Card>
        </Form>
      )}
    </Formik>
  );
};

export default Index;
