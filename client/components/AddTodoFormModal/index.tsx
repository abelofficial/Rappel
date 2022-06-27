import React, { useContext, useState } from "react";
import { Form, Formik } from "formik";
import {
  initialValues,
  validationSchema,
  fieldNames,
  fieldLabels,
} from "./addTodoFormProps";
import { CreateTodoCommand, TodoResponseDto } from "../../types";
import {
  Button,
  Card,
  Grid,
  Modal,
  Text,
  Tooltip,
  useTheme,
} from "@nextui-org/react";
import TextField, { TextAreaField } from "../TextField";
import { AuthContext, AuthContextInterface } from "../../Contexts/Auth";
import { createTodoCommand } from "../../services/commands";
import { useSWRConfig } from "swr";
import Image from "next/image";
import * as Gateway from "../../services/QueriesGateway";

export interface AddTodoFormModalProps {
  id: number;
}
const Index = ({ id }: AddTodoFormModalProps) => {
  const { theme } = useTheme();
  const { mutate } = useSWRConfig();
  const [visible, setVisible] = React.useState(false);

  const handler = () => setVisible(true);

  const closeHandler = () => {
    setVisible(false);
  };
  const [errors, setErrors] = useState<string[] | undefined>();
  const { token } = useContext<AuthContextInterface>(AuthContext);

  const onSubmitHandler = async (values: CreateTodoCommand) => {
    try {
      mutate(Gateway.UserTodoListURL(id), async (data: TodoResponseDto[]) => {
        const newTodo = await createTodoCommand(token + "", id, values);
        return [...data, newTodo];
      });

      setVisible(false);
    } catch (e: any) {
      console.log("Error: ", e);
      setErrors(e?.response?.data?.errors);
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
    <Grid.Container
      justify='center'
      gap={2}
      css={{ backgroundColor: theme?.colors.backgroundContrast }}
    >
      <Grid
        css={{ display: "flex", flexDirection: "column", alignItems: "center" }}
      >
        <Button
          auto
          css={{ bg: theme?.colors.background.value }}
          rounded
          onClick={handler}
          icon={<Image src='/add-icon.svg' alt='' width={36} height={36} />}
        />
        <Text h4>Add new task</Text>
      </Grid>
      <Modal
        noPadding
        closeButton
        blur
        aria-labelledby='modal-title'
        open={visible}
        onClose={closeHandler}
        css={{ margin: "$10" }}
      >
        <Modal.Header>
          <Text id='modal-title' size={18}>
            Add your new{" "}
            <Text b size={18}>
              todo item
            </Text>
          </Text>
        </Modal.Header>
        <Modal.Body>
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
                      name={fieldNames.title}
                      label={fieldLabels.title}
                      fullWidth
                    />
                    <TextAreaField
                      type='password'
                      name={fieldNames.description}
                      label={fieldLabels.description}
                      placeholder='Enter your amazing ideas.'
                      fullWidth
                    />

                    <Grid xs={12}>
                      <Button
                        type='submit'
                        color='success'
                        disabled={isSubmitting}
                      >
                        Create
                      </Button>
                    </Grid>
                  </Grid.Container>
                </Card>
              </Form>
            )}
          </Formik>
        </Modal.Body>
      </Modal>
    </Grid.Container>
  );
};

export default Index;
