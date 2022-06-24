import React, { useContext, useState } from "react";
import { Form, Formik } from "formik";
import {
  initialValues,
  validationSchema,
  fieldNames,
  fieldLabels,
} from "./registerProps";
import { RegisterUserRequest } from "../../types";
import { Button, Card, Grid, Text, Tooltip, useTheme } from "@nextui-org/react";
import TextField from "../TextField";
import { loginUserCommand, registerUserCommand } from "../../services/commands";
import { useRouter } from "next/router";
import { getCurrentUserQuery } from "../../services/Queries";
import { AuthContext, AuthContextInterface } from "../../Contexts/Auth";

const Index = () => {
  const { theme } = useTheme();
  const router = useRouter();
  const [errors, setErrors] = useState<string[] | undefined>();
  const { setUser, setToken } = useContext<AuthContextInterface>(AuthContext);

  const onSubmitHandler = async (values: RegisterUserRequest) => {
    try {
      await registerUserCommand(values);
      var result = await loginUserCommand(values);
      var userResp = await getCurrentUserQuery(result.data.token);
      setUser(userResp.data);
      setToken(result.data.token);

      localStorage.setItem("user", JSON.stringify(userResp.data));
      localStorage.setItem(
        userResp.data.username,
        JSON.stringify(result.data.token)
      );
      router.push("/");
    } catch (e: any) {
      setErrors(e?.response?.data.errors);
    }
  };

  const displayErrors = () => (
    <Tooltip content='' contentColor='error'>
      <Grid.Container alignItems='center' gap={1}>
        {errors?.map((e) => (
          <Grid key={e} xs={12}>
            <Text color='error'>{e}</Text>
          </Grid>
        ))}
      </Grid.Container>
    </Tooltip>
  );

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
              {errors && displayErrors()}
              <TextField
                type='text'
                name={fieldNames.firstName}
                label={fieldLabels.firstName}
                fullWidth
              />

              <TextField
                type='text'
                name={fieldNames.lastName}
                label={fieldLabels.lastName}
                fullWidth
              />
              <TextField
                type='email'
                name={fieldNames.email}
                label={fieldLabels.email}
                fullWidth
              />
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

              <TextField
                type='password'
                name={fieldNames.confirmPassword}
                label={fieldLabels.confirmPassword}
                fullWidth
              />

              <Grid xs={12}>
                <Button type='submit' color='success' disabled={isSubmitting}>
                  Register
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
